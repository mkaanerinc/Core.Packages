using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

/// <summary>
/// Represents a standardized HTTP 400 response that includes validation error details.
/// </summary>
public class ValidationProblemDetails : ProblemDetails
{
    /// <summary>
    /// Gets or sets the collection of validation errors.
    /// </summary>
    public IEnumerable<ValidationExceptionModel> Errors { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationProblemDetails"/> class 
    /// with a collection of validation errors.
    /// </summary>
    /// <param name="errors">The collection of validation errors to include in the response.</param>
    public ValidationProblemDetails(IEnumerable<ValidationExceptionModel> errors)
    {
        Title = "Validation error(s)";
        Detail = "One or more validation errors occurred.";
        Errors = errors;
        Status = StatusCodes.Status400BadRequest;
        Type = "https://example.com/probs/validation";
    }
}
