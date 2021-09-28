using System.Collections.Generic;

namespace TestCQRS3.Domain.Contracts
{
    public interface IServiceBase<TEntity>
    {
        List<TEntity> Get();
        TEntity Get(int Id);
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        bool Delete(int Id);
        bool IsExists(int Id);
    }
}
