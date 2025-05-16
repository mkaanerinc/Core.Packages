using Core.Security.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.JWT;

/// <summary>
/// Defines methods for generating access and refresh tokens.
/// </summary>
public interface ITokenHelper
{
    /// <summary>
    /// Creates an access token for the specified user with the given operation claims.
    /// </summary>
    /// <param name="user">The user for whom the token is being created.</param>
    /// <param name="operationClaims">A list of operation claims assigned to the user.</param>
    /// <returns>The generated <see cref="AccessToken"/>.</returns>
    AccessToken CreateToken(User user, IList<OperationClaim> operationClaims);

    /// <summary>
    /// Creates a refresh token for the specified user and IP address.
    /// </summary>
    /// <param name="user">The user for whom the refresh token is being created.</param>
    /// <param name="ipAddress">The IP address from which the request originated.</param>
    /// <returns>The generated <see cref="RefreshToken"/>.</returns>
    RefreshToken CreateRefreshToken(User user, string ipAddress);
}
