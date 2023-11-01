using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Repositories;
using Blogger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Blogger.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return await _unitOfWork.Repository<User>().Entities
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<UserDto> GetByEmail(string email)
        {
            return await _unitOfWork.Repository<User>().Entities
                .Include(x => x.UserRoles)
                    .ThenInclude(userRole => userRole.Role)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Email == email && x.IsDeleted == false);
        }

		public async Task<UserDto> GetById(int userId)
		{
			return await _unitOfWork.Repository<User>().Entities
                .Include(x => x.UserRoles)
					.ThenInclude(userRole => userRole.Role)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == userId && x.IsDeleted == false);
		}
	}
}
