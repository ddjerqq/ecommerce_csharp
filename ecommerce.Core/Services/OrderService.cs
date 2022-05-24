using System.Collections.Generic;
using System.Threading.Tasks;
using ecommerce.Core.Models;
using ecommerce.Core.Repositories.Interfaces;
using ecommerce.Core.Services.Interfaces;

namespace ecommerce.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        
        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }
        
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task AddAsync(Order entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order entity)
        {
            _repo.Update(entity);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order entity)
        {
            _repo.Delete(entity);
            await _repo.SaveChangesAsync();
        }
    }
}