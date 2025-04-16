using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

/// <summary>
/// Represents a standardized HTTP 400 response that includes business rule violations.
/// </summary>
public class BusinessProblemDetails : ProblemDetails
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessProblemDetails"/> class
    /// with a specific title, detail message, status code, and problem type URI.
    /// </summary>
    /// <param name="detail">A detailed description of the business rule violation.</param>
    public BusinessProblemDetails(string detail)
    {
        Title = "Rule Violation";
        Detail = detail;
        Status = StatusCodes.Status400BadRequest;
        Type = "https://example.com/probs/business";
    }
}
