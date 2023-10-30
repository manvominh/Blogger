using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Services;
using Blogger.Domain.Entities;
using Blogger.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userService.GetAll();
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto userRegistration)
        {
            var result = await _userService.Register(userRegistration);
            if (result.IsUserRegistered)
            {
                return Ok(result.Message);
            }

            ModelState.AddModelError("Email", result.Message);
            return BadRequest(ModelState);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _userService.Login(loginDto);
            if (result.IsLoginSuccess)
            {
                return Ok(result.JwtTokens);
            }
            ModelState.AddModelError("LoginError", "Invalid username or password");
            return BadRequest(ModelState);
        }
		[AllowAnonymous]
		[HttpGet("{userId}")]
		public async Task<IActionResult> GetUser(int userId)
		{
			var result = await _userService.GetUserById(userId);
			if (result != null)
			{
				return Ok(result);
			}
			ModelState.AddModelError("GetUserError", "Get User Details error.");
			return BadRequest(ModelState);
		}
		[AllowAnonymous]
        [HttpGet("GetUserDetailsByEmail/{email}")]
        public async Task<IActionResult> GetUserDetailsByEmail(string email)
        {
            var result = await _userService.GetByEmail(email);
            if (result != null)
            {
                return Ok(result);
            }
            ModelState.AddModelError("GetUserDetailsError", "Invalid Email information");
            return BadRequest(ModelState);
        }
		[Authorize]
		[HttpPut]
		public async Task<IActionResult> UpdateUser(UserDto userDto)
		{
			var result = await _userService.SaveOrUpdateUser(userDto);
			if (result.IsSavedOrUpdatedUser)
			{
				return Ok(result);
			}
			ModelState.AddModelError("SavedOrUpdatedUserError", "Saved Or Updated User Error.");
			return BadRequest(ModelState);
		}
		[Authorize]
		[HttpPost("Profile")]
		public async Task<IActionResult> UpdateProfile(UserRegistrationDto userRegistration)
		{
			var result = await _userService.UpdateProfile(userRegistration);
			if (result.IsUpdatedProfile)
			{
				return Ok(result);
			}
			ModelState.AddModelError("UpdateProfileError", "Update Profile Error.");
			return BadRequest(ModelState);
		}
		[Authorize]
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _userService.DeleteUser(id);
			if (result)
			{
				return Ok(result);
			}
			ModelState.AddModelError("DeleteUserError", "Delete User error.");
			return BadRequest(ModelState);
		}
	}
}
