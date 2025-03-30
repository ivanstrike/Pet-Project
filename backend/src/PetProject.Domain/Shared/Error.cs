﻿namespace PetProject.Domain.Shared;

public record Error
{
    public const string SEPARARTOR = "||";
    private Error(string code, string message, ErrorType type, string? invalidField = null)
    {
        Code = code;
        Message = message;
        Type = type;
        InvalidField = invalidField;
    }   
   
    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }
    public string? InvalidField { get; }

    public static Error Validation(string code, string message, string? invalidField = null) =>
        new Error(code, message, ErrorType.Validation, invalidField);
    
    public static Error NotFound(string code, string message) =>
        new Error(code, message, ErrorType.NotFound);
    
    public static Error Failure(string code, string message) =>
        new Error(code, message, ErrorType.Failure);
    
    public static Error Conflict(string code, string message) =>
        new Error(code, message, ErrorType.Conflict);
    
    public string Serialize()
    {
        return string.Join(SEPARARTOR, Code, Message, Type);
    }
    
    public static Error Deserialize(string error)
    {
        var parts = error.Split(SEPARARTOR);
        if (parts.Length < 3)
            throw new ArgumentException("Invalid serialized format");
        
        if(Enum.TryParse<ErrorType>(parts[2], out var type) == false)
            throw new ArgumentException("Invalid serialized format");
        
        return new Error(parts[0], parts[1], type);
    }
    
    public ErrorList ToErrorList() => new([this]);
}

public enum ErrorType
{
    Validation,
    NotFound,
    Failure,
    Conflict
}