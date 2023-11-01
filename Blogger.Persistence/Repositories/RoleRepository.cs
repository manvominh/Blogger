
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Repositories;
using Blogger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoleRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RoleDto>> GetAll()
        {
            return await _unitOfWork.Repository<Role>().Entities
                .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<RoleDto> GetRoleById(int roleId)
        {
            return await _unitOfWork.Repository<Role>().Entities
                //.Include(x => x.UserRoles)
                //    .ThenInclude(x => x.User)
                .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == roleId);
        }
    }
}
