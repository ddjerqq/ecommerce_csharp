using ecommerce.Core.Factories.Interfaces;
using ecommerce.Core.Models;

namespace ecommerce.Core.Factories
{
    public class OrderFactory : IOrderFactory
    {
        public Order New(int customerId, int productId, int quantity)
        {
            return new Order
            {
                Id         = GioId.New(),
                CustomerId = customerId,
                ProductId  = productId,
                Quantity   = quantity,
            };
        }
    }
}