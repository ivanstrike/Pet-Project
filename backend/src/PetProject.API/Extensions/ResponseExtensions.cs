﻿using CSharpFunctionalExtensions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PetProject.API.Response;
using PetProject.Domain.Shared;

namespace PetProject.API.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this Error error)
    {
        var statusCode = GetStatusCodeForErrorType(error.Type);
        
        var envelope = Envelope.Error(error.ToErrorList());
            
        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }
    
    public static ActionResult ToResponse(this ErrorList errors)
    {
        if (!errors.Any())
        {
            return new ObjectResult(null)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
        
        var distinctErrorTypes = errors
            .Select(e => e.Type)
            .Distinct()
            .ToList();
        
        var statusCode = distinctErrorTypes.Count > 1
            ? StatusCodes.Status500InternalServerError
            : GetStatusCodeForErrorType(distinctErrorTypes.First());

        var envelope = Envelope.Error(errors);
        return new ObjectResult(envelope)
        {
            StatusCode = statusCode
        };
    }
    
    private static int GetStatusCodeForErrorType(this ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
    }
    
   
}