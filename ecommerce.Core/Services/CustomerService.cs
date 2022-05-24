using System.Collections.Generic;
using System.Threading.Tasks;
using ecommerce.Core.Models;
using ecommerce.Core.Repositories.Interfaces;
using ecommerce.Core.Services.Interfaces;

namespace ecommerce.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;
        
        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }
        
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task AddAsync(Customer entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer entity)
        {
            _repo.Update(entity);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(Customer entity)
        {
            _repo.Delete(entity);
            await _repo.SaveChangesAsync();
        }
    }
}