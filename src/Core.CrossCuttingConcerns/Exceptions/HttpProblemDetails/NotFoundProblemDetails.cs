using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

/// <summary>
/// Represents a standardized HTTP 404 Not Found response.
/// </summary>
public class NotFoundProblemDetails : ProblemDetails
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundProblemDetails"/> class 
    /// with a predefined title, detail message, status code, and problem type URI.
    /// </summary>
    /// <param name="detail">The detail message describing the not found error.</param>
    public NotFoundProblemDetails(string detail)
    {
        Title = "Not Found";
        Detail = detail;
        Status = StatusCodes.Status404NotFound;
        Type = "https://example.com/probs/not-found";
    }
}
