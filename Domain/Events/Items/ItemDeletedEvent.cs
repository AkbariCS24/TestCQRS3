using MediatR;

namespace TestCQRS3.Domain.Events.Item
{
    public class ItemDeletedEvent : INotification
    {
        public ItemDeletedEvent(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
