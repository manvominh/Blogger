using Blogger.Application.Dtos;

namespace Blogger.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> GetUserByEmail(string email);
		Task<UserDto> GetUserById(int userId);
	}
}
