using System.Threading.Tasks;
using AutoMapper;
using ecommerce.Core.Factories.Interfaces;
using ecommerce.Core.Models;
using ecommerce.Core.Models.DataTransferObjects;
using ecommerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IOrderFactory _factory;

        public OrderController(IOrderService    orderService, 
                               ICustomerService customerService, 
                               IProductService  productService, 
                               IMapper mapper, 
                               IOrderFactory factory)
        {
            _orderService    = orderService;
            _customerService = customerService;
            _productService  = productService;
            _mapper  = mapper;
            _factory = factory;
        }
        
        [HttpGet("get_all/")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderService.GetAllAsync());
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _orderService.GetByIdAsync(id);
            return entity is null ?  NotFound() : Ok(entity);
        }
        
        [HttpPost("create/")]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto entityCreateDto)
        {
            var customer = await _customerService.GetByIdAsync(entityCreateDto.CustomerId);
            var product  = await _productService.GetByIdAsync(entityCreateDto.ProductId);

            if (customer is null)
                return BadRequest($"customer with id {entityCreateDto.CustomerId} does not exist");

            if (product is null)
                return BadRequest($"product with id {entityCreateDto.ProductId} does not exist");

            var entity = _factory.New(
                entityCreateDto.CustomerId,
                entityCreateDto.ProductId,
                entityCreateDto.Quantity);

            await _orderService.AddAsync(entity);

            return Ok(entity);
        }

        // [HttpPost("update/")]
        // public async Task<IActionResult> Update([FromBody] Order entity)
        // {
        //     await _orderService.UpdateAsync(entity);
        //     return Ok();
        // }
        //
        // [HttpDelete("delete/{id}")]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     var entity = await _orderService.GetByIdAsync(id);
        //     await _orderService.DeleteAsync(entity);
        //     return Ok();
        // }
    }
}