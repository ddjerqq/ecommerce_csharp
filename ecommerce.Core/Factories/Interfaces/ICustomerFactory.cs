using ecommerce.Core.Models;

namespace ecommerce.Core.Factories.Interfaces
{
    public interface ICustomerFactory
    {
        public Customer New(string fname, string lname, string username, string address);
    }
}