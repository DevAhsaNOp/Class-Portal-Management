namespace Application.Interfaces.Base
{
    public interface ICustomLogHandler
    {
        void LogInformation(string message, DateTime dateTime);

        void LogInformation(string message, DateTime dateTime, string stackTrace);

        void LogError(string message, DateTime dateTime);

        void LogError(string message, DateTime dateTime, string stackTrace);
    }
}
