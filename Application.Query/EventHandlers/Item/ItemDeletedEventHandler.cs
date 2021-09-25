using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Domain.Events.Item;
using MongoDB.Driver;

namespace TestCQRS3.Application.Query.EventHandlers.Item
{
    public class ItemDeletedEventHandler : INotificationHandler<ItemDeletedEvent>
    {
        private readonly ReadDBContext _context;

        public ItemDeletedEventHandler(ReadDBContext context)
        {
            _context = context;
        }

        public Task Handle(ItemDeletedEvent notification, CancellationToken cancellationToken)
        {
            _context.Item.DeleteOne(p => p.SqlId == notification.Id);

            return Task.CompletedTask;
        }
    }
}
