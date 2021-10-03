using System.Collections.Generic;
using TestCQRS3.Domain.Entities;

namespace TestCQRS3.Domain.Contracts
{
    public interface IItem2Service
    {
        List<Item2> Get();
        Item2 Get(int Id);
        Item2 Add(Item2 entity);
        void Update(Item2 entity);
        bool Delete(int Id);
        bool IsExists(int Id);
    }
}
