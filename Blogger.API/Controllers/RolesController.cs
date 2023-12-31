﻿using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _roleService.GetAll();
			return response != null ? Ok(response) : NotFound();
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
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateRole(RoleDto roleDto)
        {
            var result = await _roleService.SaveOrUpdateRole(roleDto);
            if (result.IsSavedOrUpdatedRole)
            {
                return Ok(result);
            }
            ModelState.AddModelError("SavedOrUpdatedRoleError", "Saved Or Updated Role Error.");
            return BadRequest(ModelState);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _roleService.DeleteRole(id);
            if (result)
            {
                return Ok(result);
            }
            ModelState.AddModelError("DeleteRoleError", "Delete Role error.");
            return BadRequest(ModelState);
        }
    }
}
