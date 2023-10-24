using Blogger.Application.Interfaces.Repositories;
using Blogger.Application.Interfaces.Services;
using Blogger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _roleRepository.GetAll();
        }

        public async Task<Role> GetRoleById(int roleId)
        {
            return await _roleRepository.GetRoleById(roleId);
        }
    }
}
