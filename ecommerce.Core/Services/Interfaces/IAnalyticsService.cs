using System.Threading.Tasks;
using ecommerce.Core.Models;
using ecommerce.Core.Models.DataTransferObjects;

namespace ecommerce.Core.Services.Interfaces
{
    public interface IAnalyticsService
    {
        public Task<decimal>        TotalProfit();
        public Task<ProductTypeDto> MostSoldProductType();
        public Task<Customer>       TopCustomer();
        public Task<int>            TotalOrders();
        public Task<int>            TotalProducts();
        public Task<int>            TotalCustomers();
        public Task<int>            TotalSalesByProduct(Product product);
        public Task<int>            TotalSalesByCustomer(Customer customer);
    }
}