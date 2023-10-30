using Blogger.Application.Dtos;
using Blogger.Domain.Entities;

namespace Blogger.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
		Task<User> GetUserById(int userId);
		Task<User> GetByEmail(string email);
		Task<(bool IsSavedOrUpdatedUser, string Message)> SaveOrUpdateUser(UserDto userDto);
		Task<bool> DeleteUser(int userId);
		Task<(bool IsUpdatedProfile, string Message)> UpdateProfile(UserRegistrationDto userRegistration);
		Task<(bool IsUserRegistered, string Message)> Register(UserRegistrationDto userRegistration);
        bool CheckUserUniqueEmail(string email);
        Task<(bool IsLoginSuccess, Tokens JwtTokens)> Login(LoginDto loginDto);
    }
}
