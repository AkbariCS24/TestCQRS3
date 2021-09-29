using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Domain.Contracts;
using TestCQRS3.Domain.Enums;
using TestCQRS3.Domain.Events.Item2s;

namespace TestCQRS3.Application.Query.EventHandlers.Item2
{
    public class Item2DeletedEventHandler : INotificationHandler<Item2DeletedEvent>
    {
        private readonly ReadDBContext _context;
        private readonly ILogger _logger;

        public Item2DeletedEventHandler(ReadDBContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task Handle(Item2DeletedEvent notification, CancellationToken cancellationToken)
        {
            _context.Item2.DeleteOne(p => p.SqlId == notification.Id);
            _logger.Log(LogType.Delete, "Item2Deleted", $"user delete Item2: SqlId={notification.Id}");

            return Task.CompletedTask;
        }
    }
}
