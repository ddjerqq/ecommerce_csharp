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
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;
        private readonly IProductFactory _factory;

        public ProductController(IProductService service, IMapper mapper, IProductFactory factory)
        {
            _mapper  = mapper;
            _service = service;
            _factory = factory;
        }
        
        [HttpGet("get_all/")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            return entity is null ?  NotFound() : Ok(entity);
        }
        
        [HttpPost("create/")]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto entityCreateDto)
        {
            var entity = _factory.New(
                entityCreateDto.Name, 
                entityCreateDto.Description, 
                entityCreateDto.Price);

            await _service.AddAsync(entity);

            return Ok(entity);
        }

        [HttpPost("update/")]
        public async Task<IActionResult> Update([FromBody] Product entity)
        {
            await _service.UpdateAsync(entity);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            await _service.DeleteAsync(entity);
            return Ok();
        }
    }
}