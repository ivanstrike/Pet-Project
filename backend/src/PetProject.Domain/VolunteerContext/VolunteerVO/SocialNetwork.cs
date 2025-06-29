﻿using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;
using PetProject.Domain.Shared;

namespace PetProject.Domain.VolunteerContext.VolunteerVO;

public record SocialNetwork
{
    [JsonConstructor]
    private SocialNetwork(string name, string link)
    {
        Name = name;
        Link = link;
    }
    public string Name { get; }
    public string Link { get; }

    public static Result<SocialNetwork, Error> Create(string name, string link)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsRequired("Name");
    
        if (string.IsNullOrWhiteSpace(link))
            return Errors.General.ValueIsRequired("Link");
    
        return new SocialNetwork(name, link);
    }
    
}