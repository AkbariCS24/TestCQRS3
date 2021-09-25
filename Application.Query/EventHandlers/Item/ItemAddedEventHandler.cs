using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Application.Query.QueryModel;
using TestCQRS3.Domain.Events.Item;

namespace TestCQRS3.Application.Query.EventHandlers.Item
{
    public class ItemAddedEventHandler : INotificationHandler<ItemAddedEvent>
    {
        private readonly ReadDBContext _context;

        public ItemAddedEventHandler(ReadDBContext context)
        {
            _context = context;
        }

        public Task Handle(ItemAddedEvent notification, CancellationToken cancellationToken)
        {
            var Data = notification.Data;
            ItemQueryModel Item = new()
            {
                SqlId = Data.Id,
                Field1 = Data.Field1,
                Field2 = Data.Field2,
                Field3 = Data.Field3
            };
            _context.Item.InsertOne(Item);

            return Task.CompletedTask;
        }
    }
}
