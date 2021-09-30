using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Domain.Events.Item;
using MongoDB.Driver;
using TestCQRS3.Domain.Contracts;
using TestCQRS3.Domain.Enums;

namespace TestCQRS3.Application.Query.EventHandlers.Item
{
    public class ItemDeletedEventHandler : INotificationHandler<ItemDeletedEvent>
    {
        private readonly ReadDBContext _context;
        private readonly ILogger _logger;

        public ItemDeletedEventHandler(ReadDBContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task Handle(ItemDeletedEvent notification, CancellationToken cancellationToken)
        {
            _context.Item.DeleteOne(p => p.SqlId == notification.Id);
            _context.Item2.DeleteMany(p => p.ItemId == notification.Id);
            _logger.Log(LogType.Delete, "ItemDeleted", $"user delete Item: SqlId={notification.Id}");

            return Task.CompletedTask;
        }
    }
}