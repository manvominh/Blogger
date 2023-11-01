using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Services;
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
		public async Task<IEnumerable<PostDto>> GetAllPosts()
		{
			return await _postService.GetAll();
		}
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> SavePost(PostDto post)
		{
			var result = await _postService.SavePost(post);
			if (result.IsPostSaved)
			{
				return Ok(result.post);
			}
			ModelState.AddModelError("SavePostError", "Save Post error.");
			return BadRequest(ModelState);
		}
		[HttpPut]
		[Authorize]
		public async Task<IActionResult> UpdatePost(PostDto post)
		{
			var result = await _postService.UpdatePost(post);
			if (result.IsPostUpdated)
			{
				return Ok(result.Message);
			}
			ModelState.AddModelError("UpdatedPostError", "Updated Post error.");
			return BadRequest(ModelState);
		}
		[AllowAnonymous]
        [HttpGet("{postId}")]
        public async Task<IActionResult> PostDetails(int postId)
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
		[HttpGet("GetPostsByUserId/{userId}")]
		public async Task<IActionResult> GetPostsByUserId(int userId)
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
