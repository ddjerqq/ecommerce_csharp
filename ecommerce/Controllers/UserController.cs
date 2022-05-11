using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ecommerce.Core.Factories;
using ecommerce.Core.Models;
using ecommerce.Core.Services.Interfaces;


namespace ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet("get/")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllAsync();

            if (users.Count() == 0)
            {
                return NoContent();
            }

            return Ok(users);
        }
        
        [HttpGet("get/{id}/")]
        public async Task<IActionResult> GetUser(long id)
        {
            var user = await _userService.GetByIdAsync(id);
            
            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        
        [HttpPost("create/")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            var user = UserFactory.Create(userCreateDto.Username);
            await _userService.AddAsync(user);
            return Ok();
        }

        [HttpPut("update/")]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            // var user = _mapper.Map<User>(userDto);
            await _userService.UpdateAsync(user);
            return Ok();
        }
        
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            User user = await _userService.GetByIdAsync(id);
            await _userService.DeleteAsync(user);
            return Ok();
        }

        [HttpPost("createItem/")]
        public async Task<IActionResult> CreateItem([FromBody] ItemCreateDto itemCreateDto)
        {
            var user = await _userService.GetByIdAsync(itemCreateDto.OwnerId);

            if (user is null)
            {
                return NotFound();
            }
            var item = ItemFactory.Create(itemCreateDto.Type, itemCreateDto.OwnerId);
            user.Items.ToList().Add(item);

            await _userService.UpdateAsync(user);
            
            return Ok();
        }
    }
}