using ecommerce.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Core.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {
            
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}