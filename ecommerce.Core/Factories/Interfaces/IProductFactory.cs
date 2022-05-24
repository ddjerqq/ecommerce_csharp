using ecommerce.Core.Models;

namespace ecommerce.Core.Factories.Interfaces
{
    public interface IProductFactory
    {
        public Product New(string name, string description, decimal price);
    }
}