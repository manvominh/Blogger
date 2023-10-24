using Blogger.Domain.Entities;

namespace Blogger.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAll();
        Task<Role> GetRoleById(int roleId);
    }
}
