using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Repositories;
using Blogger.Application.Interfaces.Services;
using Blogger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<RoleDto>> GetAll()
        {
            return await _roleRepository.GetAll();
        }

        public async Task<RoleDto> GetRoleById(int roleId)
        {
            return await _roleRepository.GetRoleById(roleId);
        }
        public async Task<(bool IsSavedOrUpdatedRole, string Message)> SaveOrUpdateRole(RoleDto roleDto)
        {
            var role = new Role
            {
                Id = roleDto.Id,
                Name = roleDto.Name,
                
            };
            if (role.Id != 0)
                await _unitOfWork.Repository<Role>().UpdateAsync(role);
            else
                await _unitOfWork.Repository<Role>().AddAsync(role);

            await _unitOfWork.Save();
            return (true, "Success");
        }
        public async Task<bool> DeleteRole(int roleId)
        {
            var role = await _unitOfWork.Repository<Role>().Entities.FirstOrDefaultAsync(x => x.Id == roleId);
            if (role == null) { return false; }
            await _unitOfWork.Repository<Role>().DeleteAsync(role);
            await _unitOfWork.Save();
            return true;
        }
    }
}
