using System.Collections.Generic;
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
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _service;
        private readonly ICustomerFactory _factory;

        public CustomerController(ICustomerService service, IMapper mapper, ICustomerFactory factory)
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
        public async Task<IActionResult> Create([FromBody] CustomerCreateDto entityCreateDto)
        {
            var entity = _factory.New(
                entityCreateDto.FirstName, 
                entityCreateDto.LastName, 
                entityCreateDto.Username,
                entityCreateDto.ShippingAddress);

            await _service.AddAsync(entity);

            return Ok(entity);
        }

        [HttpPost("update/")]
        public async Task<IActionResult> Update([FromBody] Customer entity)
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