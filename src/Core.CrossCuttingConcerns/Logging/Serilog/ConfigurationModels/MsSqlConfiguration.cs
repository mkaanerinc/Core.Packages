using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;

/// <summary>
/// Represents the configuration settings for connecting to an MS SQL database.
/// </summary>
public class MsSqlConfiguration
{
    /// <summary>
    /// Gets or sets the connection string used for connecting to the SQL Server database.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets the name of the table where logs or data will be stored.
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the table should be automatically created if it does not exist.
    /// </summary>
    public bool AutoCreateSqlTable { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MsSqlConfiguration"/> class.
    /// </summary>
    public MsSqlConfiguration()
    {
        ConnectionString = string.Empty;
        TableName = string.Empty;
        AutoCreateSqlTable = false;  // Default: Do not auto-create the table
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MsSqlConfiguration"/> class with specified parameters.
    /// </summary>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="tableName">The table name where data will be stored.</param>
    /// <param name="autoCreateSqlTable">Indicates whether to automatically create the table if it does not exist.</param>
    public MsSqlConfiguration(string connectionString, string tableName, bool autoCreateSqlTable)
    {
        ConnectionString = connectionString;
        TableName = tableName;
        AutoCreateSqlTable = autoCreateSqlTable;
    }
}