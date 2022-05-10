using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ecommerce.Core.Contexts;
using ecommerce.Core.Models;
using ecommerce.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Core.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;
        
        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> GetByIdAsync(long id)
        {
            return await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Item entity)
        {
            await _context.Items.AddAsync(entity);
        }

        public void Update(Item entity)
        {
            _context.Items.Update(entity);
        }

        public void Delete(Item entity)
        {
            _context.Items.Remove(entity);
        }
    }
}