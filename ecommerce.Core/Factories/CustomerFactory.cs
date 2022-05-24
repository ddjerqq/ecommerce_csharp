using ecommerce.Core.Factories.Interfaces;
using ecommerce.Core.Models;

namespace ecommerce.Core.Factories
{
    public class CustomerFactory : ICustomerFactory
    {
        public Customer New(string fname, string lname, string username, string address)
        {
            return new Customer
            {
                Id              = GioId.New(),
                FirstName       = fname,
                LastName        = lname,
                Username        = username,
                ShippingAddress = address
            };
        }
    }
}