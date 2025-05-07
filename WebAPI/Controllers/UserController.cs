using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace TheraFlow_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            return await _userService.GetAllUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> PostClient([FromForm] AddUserDto user)
        {
            try
            {
                await _userService.AddUserAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding user", ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutClient([FromForm] UserDto user)
        {
            try
            {
                await _userService.UpdateUserAsync(user);
                return CreatedAtAction(nameof(GetUser), new { id = user }, user);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating user", ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
