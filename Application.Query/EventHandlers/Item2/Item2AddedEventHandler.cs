using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Application.Query.QueryModel;
using TestCQRS3.Domain.Events.Item2s;

namespace TestCQRS3.Application.Query.EventHandlers.Item2
{
    public class Item2AddedEventHandler : INotificationHandler<Item2AddedEvent>
    {
        private readonly ReadDBContext _context;

        public Item2AddedEventHandler(ReadDBContext context)
        {
            _context = context;
        }

        public Task Handle(Item2AddedEvent notification, CancellationToken cancellationToken)
        {
            var Data = notification.Data;
            Item2QueryModel Item2 = new()
            {
                SqlId = Data.Id,
                ItemId = Data.ItemId,
                Field1 = Data.Field1,
                Field2 = Data.Field2,
                Field3 = Data.Field3
            };
            _context.Item2.InsertOne(Item2);

            return Task.CompletedTask;
        }
    }
}
