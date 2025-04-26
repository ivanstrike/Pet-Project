namespace PetProject.Application.DTO;

public record AddressDto(
    string Country,
    string City,
    string Street,
    string HouseNumber
    );