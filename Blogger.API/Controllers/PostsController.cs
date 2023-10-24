using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Services;
using Blogger.Domain.Entities;
using Blogger.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostsController : ControllerBase
	{
		private readonly IPostService _postService;

		public PostsController(IPostService postService)
		{
			_postService = postService;
		}
		[AllowAnonymous]
		[HttpGet]
		public async Task<IEnumerable<Post>> GetAllPosts()
		{
			return await _postService.GetAll();
		}
		[AllowAnonymous]
		[HttpPost]
		[Route("PostDetails")]
		public async Task<IActionResult> PostDetails([FromBody] int postId)
		{
			var result = await _postService.GetPostById(postId);
			if (result != null)
			{
				return Ok(result);
			}
			ModelState.AddModelError("PostDetailsError", "Get Post Details error.");
			return BadRequest(ModelState);
		}
		[Authorize]
		[HttpPost]
		[Route("GetPostsByUserId")]
		public async Task<IActionResult> GetPostsByUserId([FromBody] int userId)
		{
			var result = await _postService.GetPostsByUserId(userId);
			if (result != null)
			{
				return Ok(result);
			}
			ModelState.AddModelError("GetPostsByUserIdError", "Get Posts By User error.");
			return BadRequest(ModelState);
		}
	}
}
