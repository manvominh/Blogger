using Blogger.Application.Interfaces.Services;
using Blogger.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Administrator")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _roleService.GetAll();
        }
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRoleById(int roleId)
        {
            var result = await _roleService.GetRoleById(roleId);
            if (result != null)
            {
                return Ok(result);
            }
            ModelState.AddModelError("RoleDetailsError", "Get Role Details error.");
            return BadRequest(ModelState);
        }
    }
}
