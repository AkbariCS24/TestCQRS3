using MediatR;

namespace TestCQRS3.Domain.Events.Item
{
    public class ItemAddedEvent : INotification
    {
        public ItemAddedEvent(Entities.Item item)
        {
            Data = item;
        }

        public Entities.Item Data { get; set; }
    }
}
