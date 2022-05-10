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
        // private readonly IMapper _mapper;

        public UserController(IUserService userService) // IMapper mapper
        {
            _userService = userService;
            // _mapper = mapper;
        }

        [HttpGet("get/")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllAsync();

            if (users.Count() == 0)
            {
                return NoContent();
            }

            // var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
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

            // var userDto = _mapper.Map<UserDto>(user);
            return Ok(user);
        }
        
        [HttpPost("create/{username}")]
        public async Task<IActionResult> CreateUser(string username)
        {
            User user = UserFactory.Create(username);
            await _userService.AddAsync(user);
            return Ok(user);
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

        [HttpPost("createItem/{type}&{ownerId}")]
        public async Task<IActionResult> CreateItem(ItemType type, long ownerId)
        {
            var user = await _userService.GetByIdAsync(ownerId);

            if (user is null)
            {
                return NotFound();
            }
            var item = ItemFactory.Create(type, ownerId);
            user.Items.ToList().Add(item);

            await _userService.UpdateAsync(user);
            
            return Ok(item);
        }
    }
}