using Blogger.Application.Dtos;

namespace Blogger.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> GetByEmail(string email);
		Task<UserDto> GetById(int userId);
	}
}
