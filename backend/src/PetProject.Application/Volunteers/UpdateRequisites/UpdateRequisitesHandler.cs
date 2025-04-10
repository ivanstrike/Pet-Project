using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProject.Application.Extensions;
using PetProject.Application.Volunteers.UpdateSocialMedia;
using PetProject.Domain.Shared;
using PetProject.Domain.Shared.Value_Objects;
using PetProject.Domain.Volunteers;

namespace PetProject.Application.Volunteers.UpdateRequisites;

public class UpdateRequisitesHandler
{
    private IVolunteersRepository _volunteersRepository;
    private IValidator<UpdateRequisitesCommand> _validator;
    private ILogger<UpdateRequisitesHandler> _logger;

    public UpdateRequisitesHandler(
        IVolunteersRepository volunteersRepository, 
        IValidator<UpdateRequisitesCommand> validator, 
        ILogger<UpdateRequisitesHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateRequisitesCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }
        var volunteerId = VolunteerId.Create(command.Id);
        var volunteerResult = await _volunteersRepository.GetById(volunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
        {
            return volunteerResult.Error.ToErrorList();
        }
        
        List<Requisites> requisites = new();
        foreach (var requisite in command.Requisites)
        {
            var requisitesResult = Requisites.Create(
                requisite.Name, 
                requisite.Description);
            if (requisitesResult.IsFailure)
            {
                return requisitesResult.Error.ToErrorList();
            }
            requisites.Add(requisitesResult.Value);
        }
        
        volunteerResult.Value.UpdateRequisites(requisites);
        
        var result = await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);
        _logger.LogInformation(
            "Requisites for volunteer with id:{Id} updated successfully",
            result);
        return result;
    }
}