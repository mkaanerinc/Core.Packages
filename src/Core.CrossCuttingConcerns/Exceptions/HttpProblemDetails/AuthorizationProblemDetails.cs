using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

/// <summary>
/// Represents problem details specific to authorization failures (HTTP 403 Forbidden).
/// </summary>
public class AuthorizationProblemDetails : ProblemDetails
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizationProblemDetails"/> class 
    /// with a predefined title, detail message, status code, and problem type URI.
    /// </summary>
    /// <param name="detail">The detail message describing the authorization error.</param>
    public AuthorizationProblemDetails(string detail)
    {
        Title = "Authorization";
        Detail = detail;
        Status = StatusCodes.Status403Forbidden;
        Type = "https://example.com/probs/authorization";
    }
}
