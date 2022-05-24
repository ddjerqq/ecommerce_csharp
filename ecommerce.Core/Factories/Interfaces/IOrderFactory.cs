using ecommerce.Core.Models;

namespace ecommerce.Core.Factories.Interfaces
{
    public interface IOrderFactory
    {
        public Order New(int customerId, int productId, int quantity);
    }
}