using Blogger.Domain.Entities;

namespace Blogger.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAll();
        Task<Role> GetRoleById(int roleId);
    }
}
