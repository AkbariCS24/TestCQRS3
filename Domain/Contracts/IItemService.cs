using System.Collections.Generic;
using TestCQRS3.Domain.Entities;

namespace TestCQRS3.Domain.Contracts
{
    public interface IItemService
    {
        List<Item> Get();
        Item Get(int Id);
        Item Add(Item entity);
        void Update(Item entity);
        bool Delete(int Id);
        bool IsExists(int Id);
    }
}
