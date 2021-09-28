using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Domain.Events.Item2s;

namespace TestCQRS3.Application.Query.EventHandlers.Item2
{
    public class Item2DeletedEventHandler : INotificationHandler<Item2DeletedEvent>
    {
        private readonly ReadDBContext _context;

        public Item2DeletedEventHandler(ReadDBContext context)
        {
            _context = context;
        }

        public Task Handle(Item2DeletedEvent notification, CancellationToken cancellationToken)
        {
            _context.Item2.DeleteOne(p => p.SqlId == notification.Id);

            return Task.CompletedTask;
        }
    }
}
