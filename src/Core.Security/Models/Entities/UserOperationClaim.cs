using Core.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Models.Entities;

/// <summary>
/// Represents the relationship between a user and an operation claim, defining the permissions a user has.
/// </summary>
public class UserOperationClaim : Entity<int>
{
    /// <summary>
    /// Gets or sets the ID of the user associated with this operation claim.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the operation claim associated with this user.
    /// </summary>
    public int OperationClaimId { get; set; }

    /// <summary>
    /// The user associated with this operation claim.
    /// </summary>
    /// <remarks>
    /// This navigation property represents a many-to-one relationship where a user operation claim is associated with a single user.
    /// The property corresponds to the <see cref="UserId"/> foreign key, which is required (non-nullable) in the database schema.
    /// </remarks>
    public virtual User User { get; set; } = null!;

    /// <summary>
    /// The operation claim associated with this user.
    /// </summary>
    /// <remarks>
    /// This navigation property represents a many-to-one relationship where a user operation claim is associated with a single operation claim.
    /// This navigation property corresponds to the <see cref="OperationClaimId"/> foreign key, which is required (non-nullable) in the database schema.
    /// </remarks>
    public virtual OperationClaim OperationClaim { get; set; } = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserOperationClaim"/> class with default values.
    /// This constructor is required by Entity Framework Core for materializing entities from the database.
    /// </summary>
    public UserOperationClaim()
    {       
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserOperationClaim"/> class with the specified values.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="operationClaimId">The ID of the operation claim.</param>
    public UserOperationClaim(int userId, int operationClaimId)
    {
        UserId = userId;
        OperationClaimId = operationClaimId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserOperationClaim"/> class with the specified ID and values.
    /// </summary>
    /// <param name="id">The unique identifier of the user operation claim.</param>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="operationClaimId">The ID of the operation claim.</param>
    public UserOperationClaim(int id, int userId, int operationClaimId) : base(id)
    {
        UserId = userId;
        OperationClaimId = operationClaimId;
    }
}
