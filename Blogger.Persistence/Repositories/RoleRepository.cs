
using Blogger.Application.Interfaces.Repositories;
using Blogger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IGenericRepository<Role> _roleRepository;

        public RoleRepository(IGenericRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _roleRepository.GetAllAsync();
        }

        public async Task<Role> GetRoleById(int roleId)
        {
            return await _roleRepository.Entities.FirstOrDefaultAsync(x => x.Id == roleId);
        }
    }
}
