using MediatR;

namespace TestCQRS3.Domain.Events.Item
{
    public class ItemUpdatedEvent : INotification
    {
        public ItemUpdatedEvent(Entities.Item item)
        {
            Data = item;
        }
        public Entities.Item Data { get; set; }
    }
}
