using Blogger.Application.Dtos;
using Blogger.Domain.Entities;

namespace Blogger.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAll();
        Task<RoleDto> GetRoleById(int roleId);
        Task<(bool IsSavedOrUpdatedRole, string Message)> SaveOrUpdateRole(RoleDto roleDto);
        Task<bool> DeleteRole(int roleId);
    }
}
