using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Domain.Events.Item;
using MongoDB.Driver;
using TestCQRS3.Application.Query.QueryModel;
using TestCQRS3.Domain.Enums;
using TestCQRS3.Domain.Contracts;

namespace TestCQRS3.Application.Query.EventHandlers.Item
{
    public class ItemUpdatedEventHandler : INotificationHandler<ItemUpdatedEvent>
    {
        private readonly ReadDBContext _context;
        private readonly ILogger _logger;

        public ItemUpdatedEventHandler(ReadDBContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task Handle(ItemUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var Data = notification.Data;
            ItemQueryModel newItem = new()
            {
                SqlId = Data.Id,
                Field1 = Data.Field1,
                Field2 = Data.Field2,
                Field3 = Data.Field3
            };

            ItemQueryModel oldItem = _context.Item.Find(p => p.SqlId == Data.Id).FirstOrDefault();
            newItem.Id = oldItem.Id;
            _context.Item.ReplaceOne(p => p.SqlId == newItem.SqlId, newItem);
            _logger.Log(LogType.Update, "ItemUpdated", $"user Update Item: SqlId:{Data.Id}, Field1: {oldItem.Field1} => {Data.Field1}, Field2: {oldItem.Field2} => {Data.Field2}, Field3: {oldItem.Field3} => {Data.Field3}");

            return Task.CompletedTask;
        }
    }
}
