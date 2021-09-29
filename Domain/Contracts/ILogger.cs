using TestCQRS3.Domain.Enums;

namespace TestCQRS3.Domain.Contracts
{
    public interface ILogger
    {
        void Log(LogType logType, string Title, string Text);
    }
}
