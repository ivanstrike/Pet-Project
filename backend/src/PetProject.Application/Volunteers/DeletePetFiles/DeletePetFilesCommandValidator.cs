using FluentValidation;
using FluentValidation.Validators;
using PetProject.Application.Validation;
using PetProject.Domain.Shared;

namespace PetProject.Application.Volunteers.DeletePetFiles;

public class DeletePetFilesCommandValidator : AbstractValidator<DeletePetFilesCommand>
{
    public DeletePetFilesCommandValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(x => x.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleForEach(x => x.FileNames).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}