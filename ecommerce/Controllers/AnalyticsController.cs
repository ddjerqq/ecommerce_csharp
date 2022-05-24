using System.Threading.Tasks;
using ecommerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly ICustomerService  _customerService;
        private readonly IProductService   _productService;
        
        public AnalyticsController(IAnalyticsService analyticsService, ICustomerService customerService, IProductService productService)
        {
            _analyticsService = analyticsService;
            _customerService  = customerService;
            _productService   = productService;
        }
            
        [HttpGet("total_profit/")]
        public async Task<IActionResult> TotalProfit()
        {
            return Ok(await _analyticsService.TotalProfit());
        }

        [HttpGet("most_sold_product_type/")]
        public async Task<IActionResult> MostSoldProductType()
        {
            return Ok(await _analyticsService.MostSoldProductType());
        }
        
        [HttpGet("top_customer/")]
        public async Task<IActionResult> TopCustomer()
        {
            return Ok(await _analyticsService.TopCustomer());
        }
        
        [HttpGet("total_orders/")]
        public async Task<IActionResult> TotalOrders()
        {
            return Ok(await _analyticsService.TotalOrders());
        }
        
        [HttpGet("total_products/")]
        public async Task<IActionResult> TotalProducts()
        {
            return Ok(await _analyticsService.TotalProducts());
        }
        
        [HttpGet("total_customers/")]
        public async Task<IActionResult> TotalCustomers()
        {
            return Ok(await _analyticsService.TotalCustomers());
        }
        
        [HttpGet("total_sales_by_product/{id}")]
        public async Task<IActionResult> TotalSalesByProduct(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            
            if (product is null)
                return NotFound();
            
            return Ok(await _analyticsService.TotalSalesByProduct(product));
        }

        [HttpGet("total_sales_by_customer/{id}")]
        public async Task<IActionResult> TotalSalesByCustomer(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            
            if (customer is null)
                return NotFound();
            
            return Ok(await _analyticsService.TotalSalesByCustomer(customer));
        }
    }
}