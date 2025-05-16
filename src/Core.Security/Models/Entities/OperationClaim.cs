using Core.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Models.Entities;

/// <summary>
/// Represents an operation claim that defines a specific permission or role in the system.
/// </summary>
public class OperationClaim : Entity<int>
{
    /// <summary>
    /// Gets or sets the name of the operation claim.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The collection of user operation claims associated with this operation claim.
    /// </summary>
    /// <remarks>
    /// This navigation property represents a one-to-many relationship where a single operation claim can be associated with multiple user operation claims.
    /// The collection is initialized as an empty <see cref="HashSet{T}"/> to prevent null reference issues and ensure unique user operation claims.
    /// </remarks>
    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = new HashSet<UserOperationClaim>();

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationClaim"/> class with default values.
    /// This constructor is required by Entity Framework Core for materializing entities from the database.
    /// </summary>
    public OperationClaim()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationClaim"/> class with the specified name.
    /// </summary>
    /// <param name="name">The name of the operation claim.</param>
    public OperationClaim(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OperationClaim"/> class with the specified ID and name.
    /// </summary>
    /// <param name="id">The unique identifier of the operation claim.</param>
    /// <param name="name">The name of the operation claim.</param>
    public OperationClaim(int id, string name) : base(id)
    {
        Name = name;
    }
}
