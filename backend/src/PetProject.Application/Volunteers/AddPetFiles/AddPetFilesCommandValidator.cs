using FluentValidation;
using PetProject.Application.Validation;
using PetProject.Domain.Shared;

namespace PetProject.Application.Volunteers.AddPetFiles;

public class AddPetFilesCommandValidator: AbstractValidator<AddPetFilesCommand>
{
   public AddPetFilesCommandValidator()
   {
      RuleFor(p => p.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
      RuleFor(p => p.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
      RuleForEach(p => p.Files).SetValidator(new UploadFileDtoValidator());
   }
}