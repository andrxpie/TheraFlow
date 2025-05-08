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
        public async Task<ActionResult<UserDto>> GetUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost("admin")]
        public async Task<IActionResult> PostAdminUser([FromForm] RegisterAdminDto user)
        {
            try
            {
                await _userService.RegisterAdminAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding admin", ex);
            }
        }

        [HttpPost("client")]
        public async Task<IActionResult> PostClientUser([FromForm] RegisterDefaultUserDto user)
        {
            try
            {
                await _userService.RegisterClientAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding client", ex);
            }
        }

        [HttpPost("specialist")]
        public async Task<IActionResult> PostSpecialistUser([FromForm] RegisterDefaultUserDto user)
        {
            try
            {
                await _userService.RegisterSpecialistAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding specialist", ex);
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
        public async Task<IActionResult> DeleteClient(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpPost("login-via-email")]
        public async Task<IActionResult> LoginViaEmail([FromBody] LoginViaEmailDto model)
        {
            try
            {
                var loginResponse = await _userService.LoginViaEmailAsync(model);
                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Error logging in via email", ex);
            }
        }

        [HttpPost("login-via-username")]
        public async Task<IActionResult> LoginViaUserName([FromBody] LoginViaUserNameDto model)
        {
            try
            {
                var loginResponse = await _userService.LoginViaUserNameAsync(model);
                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Error logging in via username", ex);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutDto model)
        {
            await _userService.Logout(model.RefreshToken);
            return Ok();
        }
    }
}
