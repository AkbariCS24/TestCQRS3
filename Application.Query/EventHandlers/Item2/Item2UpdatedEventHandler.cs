using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Domain.Events.Item2s;
using MongoDB.Driver;
using TestCQRS3.Application.Query.QueryModel;

namespace TestCQRS3.Application.Query.EventHandlers.Item2
{
    public class Item2UpdatedEventHandler : INotificationHandler<Item2UpdatedEvent>
    {
        private readonly ReadDBContext _context;

        public Item2UpdatedEventHandler(ReadDBContext context)
        {
            _context = context;
        }

        public Task Handle(Item2UpdatedEvent notification, CancellationToken cancellationToken)
        {
            var Data = notification.Data;
            Item2QueryModel newItem2 = new()
            {
                SqlId = Data.Id,
                Field1 = Data.Field1,
                Field2 = Data.Field2,
                Field3 = Data.Field3
            };

            Item2QueryModel oldItem2 = _context.Item2.Find(p => p.SqlId == Data.Id).FirstOrDefault();
            newItem2.Id = oldItem2.Id;
            _context.Item2.ReplaceOne(p => p.SqlId == newItem2.SqlId, newItem2);

            return Task.CompletedTask;
        }
    }
}
