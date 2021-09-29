using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TestCQRS3.Domain.Events.Item2s;
using MongoDB.Driver;
using TestCQRS3.Application.Query.QueryModel;
using TestCQRS3.Domain.Contracts;
using TestCQRS3.Domain.Enums;

namespace TestCQRS3.Application.Query.EventHandlers.Item2
{
    public class Item2UpdatedEventHandler : INotificationHandler<Item2UpdatedEvent>
    {
        private readonly ReadDBContext _context;
        private readonly ILogger _logger;

        public Item2UpdatedEventHandler(ReadDBContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
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
            _logger.Log(LogType.Update, "Item2Updated", $"user Update Item2: SqlId:{Data.Id}, ItemId: {oldItem2.ItemId} => {Data.ItemId} Field1: {oldItem2.Field1} => {Data.Field1}, Field2: {oldItem2.Field2} => {Data.Field2}, Field3: {oldItem2.Field3} => {Data.Field3}");

            return Task.CompletedTask;
        }
    }
}
