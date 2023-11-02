using Blogger.Application.Dtos;

namespace Blogger.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
		Task<UserDto> GetUserById(int userId);
		Task<UserDto> GetUserByEmail(string email);
		Task<(bool IsSavedOrUpdatedUser, string Message)> SaveOrUpdateUser(UserDto userDto);
		Task<bool> DeleteUser(int userId);
		Task<(bool IsUpdatedProfile, string Message)> UpdateProfile(UserRegistrationDto userRegistration);
		Task<(bool IsUserRegistered, string Message)> Register(UserRegistrationDto userRegistration);
        bool CheckUserUniqueEmail(string email);
        Task<(bool IsLoginSuccess, Tokens JwtTokens)> Login(LoginDto loginDto);
    }
}
