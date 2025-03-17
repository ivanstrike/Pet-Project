﻿using CSharpFunctionalExtensions;

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

    public static Result Create(string country, string city, string street, string houseNumber)
    {
        if (string.IsNullOrWhiteSpace(country) || string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(street) || string.IsNullOrWhiteSpace(houseNumber))
        {
            return Result.Failure<Size>("Adresses are missing.");
        }
        
        var address = new Address(country, city, street, houseNumber);
        return Result.Success(address);
    }
    
}