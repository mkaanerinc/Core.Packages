using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

/// <summary>
/// Represents a standardized HTTP 500 Internal Server Error response.
/// </summary>
public class InternalServerErrorProblemDetails : ProblemDetails
{

    /// <summary>
    /// Initializes a new instance of the <see cref="InternalServerErrorProblemDetails"/> class 
    /// with a predefined title, detail message, status code, and problem type URI.
    /// </summary>
    /// <param name="detail">The detail message describing the internal server error.</param>
    public InternalServerErrorProblemDetails(string detail)
    {
        Title = "Internal Server Error";
        Detail = "Internal Server Error";
        Status = StatusCodes.Status500InternalServerError;
        Type = "https://example.com/probs/internal";
    }
}
