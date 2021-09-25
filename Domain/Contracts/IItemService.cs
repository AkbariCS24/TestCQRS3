using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCQRS3.Domain.Entities;

namespace TestCQRS3.Domain.Contracts
{
    public interface IItemService
    {
        List<Item> Get();
        Item Get(int Id);
        Item Add(Item item);
        void Update(Item item);
        bool Delete(int Id);
        bool IsExists(int Id);
    }
}
