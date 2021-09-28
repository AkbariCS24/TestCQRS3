namespace TestCQRS3.Domain.Entities
{
    public class Item2
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Field1 { get; set; }
        public bool Field2 { get; set; }
        public string Field3 { get; set; }

        public Item Item { get; set; }
    }
}
