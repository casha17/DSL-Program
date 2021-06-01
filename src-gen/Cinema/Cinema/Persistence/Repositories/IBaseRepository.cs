using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cinema.Persistence.Models;

namespace Cinema.Persistence.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : IEntity
    {
        Task<bool> Delete(Guid id);
        Task<IEnumerable<TEntity>> GetPaged(int page, int pageSize);
        Task<TEntity> GetById(Guid id);
        Task<Guid> Insert(TEntity entity);
        Task<TEntity> Put(TEntity entity);
    }
}
