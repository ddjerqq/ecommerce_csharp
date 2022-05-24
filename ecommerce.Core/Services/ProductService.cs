using System.Collections.Generic;
using System.Threading.Tasks;
using ecommerce.Core.Models;
using ecommerce.Core.Repositories.Interfaces;
using ecommerce.Core.Services.Interfaces;

namespace ecommerce.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }
        
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task AddAsync(Product entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
             _repo.Update(entity);
             await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product entity)
        {
            _repo.Delete(entity);
            await _repo.SaveChangesAsync();
        }
    }
}