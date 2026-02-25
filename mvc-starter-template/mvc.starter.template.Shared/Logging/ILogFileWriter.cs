namespace mvc.starter.template.Shared.Logging
{
    public interface ILogFileWriter
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message, Exception? exception = null);
        string GetLogDirectory();
    }
}
