using MediatR;

namespace TestCQRS3.Domain.Events.Item2s
{
    public class Item2DeletedEvent : INotification
    {
        public Item2DeletedEvent(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
