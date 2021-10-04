using System;

namespace TestCQRS3.Domain.Entities
{
    public class Item2 : BaseEntity
    {
        protected Item2() { }

        private Item2(int itemId, string field1,bool field2, string field3)
        {
            ItemId = itemId;
            Field1 = field1;
            Field2 = field2;
            Field3 = field3;
        }

        private Item2(int id, int itemId, string field1, bool field2, string field3)
        {
            Id = id;
            ItemId = itemId;
            Field1 = field1;
            Field2 = field2;
            Field3 = field3;
        }

        public int ItemId { get; set; }
        public string Field1 { get; set; }
        public bool Field2 { get; set; }
        public string Field3 { get; set; }

        public Item Item { get; set; }

        public static Item2 CreateNewItem2(int itemId, string field1, bool field2, string field3)
        {
            return new Item2(itemId,field1, field2, field3);
        }

        public static Item2 CreateItem2(int id, int itemId, string field1, bool field2, string field3)
        {
            return new Item2(id, itemId, field1, field2, field3);
        }

        public void SetModifyDate(DateTime dateTime)
        {
            ModifyDate = dateTime;
        }
    }
}
