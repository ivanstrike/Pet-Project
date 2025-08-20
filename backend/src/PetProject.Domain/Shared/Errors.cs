using PetProject.Domain.VolunteerContext.PetVO;

namespace PetProject.Domain.Shared;

public class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation("value.is.invalid", $"{label} is invalid");
        }
        
        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $"for id {id}";
            return Error.NotFound("record.not.found", $"record not found {forId}");
        }
        
        public static Error ValueIsRequired(string? name = null)
        {
            var label = name ?? "";
            return Error.NotFound("value.is.invalid", $"{label} is required");
        }
    }
    
    public static class Volunteer
    {
        public static Error EmailAlreadyExists(string email)
        {
            return Error.Validation("email.already.exists", $"User with email: {email} already exists");
        }
        public static Error PetNotFound(SerialNumber? serialNumber = null)
        {
            var forSerialNumber = serialNumber == null ? "" : $"for serialNumber {serialNumber.Value}";
            return Error.NotFound("record.not.found", $"record not found {forSerialNumber}");
        }
        
    }
}