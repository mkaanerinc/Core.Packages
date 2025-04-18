using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;

/// <summary>
/// Represents configuration settings for file-based logging.
/// </summary>
public class FileLogConfiguration
{
    /// <summary>
    /// Gets or sets the folder path where log files will be stored.
    /// </summary>
    public string FolderPath { get; set; }

    /// <summary>
    /// Name format of the log file (supports Serilog rolling file tokens).
    /// Default: log-.txt
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// File rolling interval (e.g., Day, Hour, etc.).
    /// </summary>
    public string RollingInterval { get; set; }

    /// <summary>
    /// Maximum number of retained log files.
    /// </summary>
    public int RetainedFileCountLimit { get; set; }

    /// <summary>
    /// Maximum file size in bytes before rolling occurs.
    /// </summary>
    public long FileSizeLimitBytes { get; set; }

    /// <summary>
    /// Whether to roll when file size exceeds the limit.
    /// </summary>
    public bool RollOnFileSizeLimit { get; set; }

    /// <summary>
    /// Whether the log file can be shared across processes.
    /// </summary>
    public bool Shared { get; set; }

    /// <summary>
    /// Template that defines the format of each log entry.
    /// </summary>
    public string OutputTemplate { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileLogConfiguration"/> class with default values.
    /// </summary>
    public FileLogConfiguration()
    {
        FolderPath = "logs";
        FileName = "log-.txt";
        RollingInterval = "Day";
        RetainedFileCountLimit = 7;
        FileSizeLimitBytes = 5000000;
        RollOnFileSizeLimit = true;
        Shared = true;
        OutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileLogConfiguration"/> class with the specified logging configuration parameters.
    /// </summary>
    /// <param name="folderPath">The path to the folder where log files will be saved.</param>
    /// <param name="fileName">The name format of the log file (e.g., "log-.txt").</param>
    /// <param name="rollingInterval">The time interval to roll log files (e.g., "Day", "Hour").</param>
    /// <param name="retainedFileCountLimit">The maximum number of log files to retain.</param>
    /// <param name="fileSizeLimitBytes">The maximum size (in bytes) of a single log file before rolling occurs.</param>
    /// <param name="rollOnFileSizeLimit">Indicates whether to roll the file when it exceeds the specified size.</param>
    /// <param name="shared">Indicates whether the log file can be shared across multiple processes.</param>
    /// <param name="outputTemplate">The template that defines the format of log messages.</param>
    public FileLogConfiguration(string folderPath, string fileName, string rollingInterval, int retainedFileCountLimit,
        long fileSizeLimitBytes, bool rollOnFileSizeLimit, bool shared, string outputTemplate)
    {
        FolderPath = folderPath;
        FileName = fileName;
        RollingInterval = rollingInterval;
        RetainedFileCountLimit = retainedFileCountLimit;
        FileSizeLimitBytes = fileSizeLimitBytes;
        RollOnFileSizeLimit = rollOnFileSizeLimit;
        Shared = shared;
        OutputTemplate = outputTemplate;
    }
}