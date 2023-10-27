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
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveComment(CommentDto commentDto)
        {
            var result = await _commentService.SaveComment(commentDto);
            if (result.IsCommentSaved)
            {
                return Ok(result.comment);
            }
            ModelState.AddModelError("SavePostError", "Save Post error.");
            return BadRequest(ModelState);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _commentService.DeleteComment(id);
            if (result)
            {
                return Ok(result);
            }
            ModelState.AddModelError("DeleteCommentError", "Delete Comment error.");
            return BadRequest(ModelState);
        }
    }
}
