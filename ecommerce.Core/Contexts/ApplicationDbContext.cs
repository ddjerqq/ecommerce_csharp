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
        
        public DbSet<Product>  Products  { get; set; }
        public DbSet<Order>    Orders    { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}