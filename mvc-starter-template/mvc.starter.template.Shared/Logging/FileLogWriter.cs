using System.Text;
using Microsoft.Extensions.Options;
using mvc.starter.template.Shared.Logging;

namespace mvc.starter.template.Application.Logging;

public sealed class FileLogWriter : ILogFileWriter
{
    private static readonly object _lock = new();

    public void LogInfo(string message)
        => Write("INFO", message);

    public void LogWarning(string message)
        => Write("WARN", message);

    public void LogError(string message, Exception? exception = null)
    {
        var fullMessage = exception == null
            ? message
            : $"{message}{Environment.NewLine}{exception}";

        Write("ERROR", fullMessage);
    }

    public string GetLogDirectory()
    {
        var basePath = AppContext.BaseDirectory;

        var directory = Path.Combine(
            basePath,
            "Logs"
        );

        return directory;
    }

    private void Write(string level, string message)
    {
        try
        {
            var directory = GetLogDirectory();           

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var fileName = $"{DateTime.Now:yyyyMMdd}.txt";

            var filePath = Path.Combine(directory, fileName);

            var logLine = BuildLogLine(level, message);

            Console.WriteLine(logLine);

            lock (_lock)
            {
                File.AppendAllText(filePath, logLine, Encoding.UTF8);
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Log writer error: " + ex.Message);
        }
    }

    private static string BuildLogLine(string level, string message)
    {
        return
$"""
[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}]
{message}
--------------------------------------------------
""";
    }
}
