using Blogger.Application.Dtos;

namespace Blogger.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<RoleDto>> GetAll();
        Task<RoleDto> GetRoleById(int roleId);
    }
}
