using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetProject.Application.Database;
using PetProject.Application.Extensions;
using PetProject.Domain.Shared;
using PetProject.Domain.Volunteers;
using PetProject.Domain.Volunteers.Value_Objects;

namespace PetProject.Application.Volunteers.UpdateSocialMedia;

public class UpdateSocialMediaHandler
{
    private IVolunteersRepository _volunteersRepository;
    private IValidator<UpdateSocialMediaCommand> _validator;
    private ILogger<UpdateSocialMediaHandler> _logger;

    public UpdateSocialMediaHandler(
        IVolunteersRepository volunteersRepository, 
        IValidator<UpdateSocialMediaCommand> validator, 
        ILogger<UpdateSocialMediaHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateSocialMediaCommand command,
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
        
        List<SocialNetwork> socialNetworks = [];
        foreach (var socialNetwork in command.SocialNetworks)
        {
            var socialNetworkResult = SocialNetwork.Create(
                socialNetwork.Name, 
                socialNetwork.Link);
            if (socialNetworkResult.IsFailure)
            {
                return socialNetworkResult.Error.ToErrorList();
            }
            socialNetworks.Add(socialNetworkResult.Value);
        }
        
        volunteerResult.Value.UpdateSocialMedia(socialNetworks);
        
        var result = await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);
        _logger.LogInformation(
            "Social networks for volunteer with id:{Id} updated successfully",
            result);
        
        return result;
    }
}