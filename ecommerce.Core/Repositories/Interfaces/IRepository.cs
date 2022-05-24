using System.Collections.Generic;
using System.Threading.Tasks;

namespace ecommerce.Core.Repositories.Interfaces
{
    public interface IRepository<TEntity>
    {
        public Task SaveChangesAsync();
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public Task<TEntity> GetByIdAsync(int id);
        public Task AddAsync(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
    }
}