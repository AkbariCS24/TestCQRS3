using System.Collections.Generic;

namespace TestCQRS3.Domain.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }

        public ICollection<Item2> Item2s { get; set; }
    }
}
