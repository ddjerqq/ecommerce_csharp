using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecommerce.Core.Repositories.Interfaces
{
    public interface IRepository<TEntity>
    {
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public Task<TEntity> GetByIdAsync(long id);
        public Task AddAsync(TEntity entity);
        public Task Update(TEntity entity);
        public Task Delete(TEntity entity);
    }
}