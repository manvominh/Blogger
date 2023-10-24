using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Services;
using Blogger.Domain.Entities;
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
                return Ok(result.UserSession);
            }
            ModelState.AddModelError("LoginError", "Invalid username or password");
            return BadRequest(ModelState);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("GetUserDetailsByEmail")]
        public async Task<IActionResult> GetUserDetailsByEmail([FromBody] string email)
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
		[HttpPost]
		[Route("UpdateUserByEmail")]
		public async Task<IActionResult> UpdateUserByEmail([FromBody] UserRegistrationDto userRegistration)
		{
			var result = await _userService.UpdateUserByEmail(userRegistration);
			if (result.IsUserUpdated)
			{
				return Ok(result);
			}
			ModelState.AddModelError("UpdateProfileError", "Update Profile Error.");
			return BadRequest(ModelState);
		}
	}
}
