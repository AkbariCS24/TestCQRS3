using TestCQRS3.Domain.Contracts;
using TestCQRS3.Domain.Enums;
using TestCQRS3.Infrastructure.Logging.Models;

namespace TestCQRS3.Infrastructure.Logging
{
    public class Logger : ILogger
    {
        private readonly LogDBContext _context;

        public Logger(LogDBContext context)
        {
            _context = context;
        }

        public void Log(LogType logType, string Title, string Text)
        {
            LogModel logModel = new()
            {
                LogType = logType.ToString(),
                Title = Title,
                Text = Text
            };
            _context.Log.InsertOne(logModel);
        }
    }
}
