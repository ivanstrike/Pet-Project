using FluentValidation;
using PetProject.Application.DTO;
using PetProject.Domain.Shared;

namespace PetProject.Application.Validation;

public class UploadFileDtoValidator: AbstractValidator<UploadFileDto>
{
    public UploadFileDtoValidator()
    {
        RuleFor(u => u.Content).Must(c => c.Length > 0 && c.Length < 10000000);
        RuleFor(u => u.FileName)
            .Must(f => f.Contains(".jpg") || f.Contains(".png"))
            .WithError(Errors.General.ValueIsInvalid("FileName"))
            .NotEmpty()
            .WithError(Errors.General.ValueIsRequired("FileName"));
    }
    
}