using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Extensions;
using Core.Security.Models.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Authorization;

/// <summary>
/// Represents a MediatR pipeline behavior that handles authorization based on user roles.
/// </summary>
/// <typeparam name="TRequest">The type of request.</typeparam>
/// <typeparam name="TResponse">The type of response.</typeparam>
public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizationBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="httpContextAccessor">Provides access to the current HTTP context.</param>
    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Handles authorization logic before executing the request handler.
    /// </summary>
    /// <param name="request">The request message.</param>
    /// <param name="next">The next handler in the pipeline.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The response from the next handler if authorization succeeds.</returns>
    /// <exception cref="AuthorizationException">Thrown if the user is not authenticated or not authorized.</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        List<string>? userRoleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();

        if (userRoleClaims == null)
            throw new AuthorizationException("You are not authenticated.");

        bool isNotMatchedAUserRoleClaimWithRequestRoles = userRoleClaims
            .FirstOrDefault(
                userRoleClaim => userRoleClaim == GeneralOperationClaims.Admin || request.Roles.Any(role => role == userRoleClaim)
            )
            .IsNullOrEmpty();

        if (isNotMatchedAUserRoleClaimWithRequestRoles)
            throw new AuthorizationException("You are not authorized.");

        TResponse response = await next();
        return response;
    }
}
