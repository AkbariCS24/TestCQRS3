using System;
using System.Collections.Generic;

namespace TestCQRS3.Domain.Entities
{
    public class Item : BaseEntity
    {
        protected Item() { }

        private Item(string field1, string field2, string field3)
        {
            Field1 = field1;
            Field2 = field2;
            Field3 = field3;
        }

        private Item(int id, string field1, string field2, string field3)
        {
            Id = id;
            Field1 = field1;
            Field2 = field2;
            Field3 = field3;
        }

        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }

        public ICollection<Item2> Item2s { get; set; }

        public static Item CreateNewItem(string field1, string field2, string field3)
        {
            return new Item(field1, field2, field3);
        }

        public static Item CreateItem(int id, string field1, string field2, string field3)
        {
            return new Item(id, field1, field2, field3);
        }

        public void SetModifyDate(DateTime dateTime)
        {
            ModifyDate = dateTime;
        }
    }
}
