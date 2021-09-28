using MediatR;
using TestCQRS3.Domain.Entities;

namespace TestCQRS3.Domain.Events.Item2s
{
    public class Item2UpdatedEvent : INotification
    {
        public Item2UpdatedEvent(Item2 item2)
        {
            Data = item2;
        }
        public Item2 Data { get; set; }
    }
}
