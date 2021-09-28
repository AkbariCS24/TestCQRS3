using MediatR;
using TestCQRS3.Domain.Entities;

namespace TestCQRS3.Domain.Events.Item2s
{
    public class Item2AddedEvent : INotification
    {
        public Item2AddedEvent(Item2 item2)
        {
            Data = item2;
        }

        public Item2 Data { get; set; }
    }
}
