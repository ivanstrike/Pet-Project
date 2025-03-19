using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain;

public struct Address
{
    public string Country { get; }
    public string City { get; }
    public string Street { get; }
    public string HouseNumber { get; }

    private Address(string country, string city, string street, string houseNumber)
    {
        Country = country;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
    }

    public static Result<Address, Error> Create(string country, string city, string street, string houseNumber)
    {
        if (string.IsNullOrWhiteSpace(country))
        {
            return Errors.General.ValueIsRequired("Country");
        }
        
        if (string.IsNullOrWhiteSpace(city))
        {
            return Errors.General.ValueIsRequired("City");
        }
        
        if (string.IsNullOrWhiteSpace(street))
        {
            return Errors.General.ValueIsRequired("Street");
        }
        
        if (string.IsNullOrWhiteSpace(houseNumber))
        {
            return Errors.General.ValueIsRequired("HouseNumber");
        }
        
        if (country.Length > Constants.MAX_LOW_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Country");
        }

        if (city.Length > Constants.MAX_LOW_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("City");
        }

        if (street.Length > Constants.MAX_LOW_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("Street");
        }

        if (houseNumber.Length > Constants.MAX_LOW_TEXT_LENGTH)
        {
            return Errors.General.ValueIsInvalid("HouseNumber");
}
        
        return new Address(country, city, street, houseNumber);
    }
    
}