using ecommerce.Core.Factories.Interfaces;
using ecommerce.Core.Models;

namespace ecommerce.Core.Factories
{
    public class ProductFactory : IProductFactory
    {
        public Product New(string name, string description, decimal price)
        {
            return new Product
            {
                Id          = GioId.New(),
                Name        = name,
                Description = description,
                Price       = price,
            };
        }
    }
}