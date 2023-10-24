using Blogger.Application.Dtos;
using Blogger.Domain.Entities;

namespace Blogger.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByEmail(string email);
		Task<(bool IsUserUpdated, string Message)> UpdateUserByEmail(UserRegistrationDto userRegistration);

		Task<(bool IsUserRegistered, string Message)> Register(UserRegistrationDto userRegistration);

        bool CheckUserUniqueEmail(string email);

        Task<(bool IsLoginSuccess, UserSession UserSession)> Login(LoginDto loginDto);
    }
}
