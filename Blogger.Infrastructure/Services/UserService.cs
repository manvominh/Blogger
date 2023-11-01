using Blogger.Application.Common.Enums;
using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Repositories;
using Blogger.Application.Interfaces.Services;
using Blogger.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Blogger.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtAuthenticationManagerService _jwtAuthenticationManagerService;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork
            , IJwtAuthenticationManagerService jwtAuthenticationManagerService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _jwtAuthenticationManagerService = jwtAuthenticationManagerService;
        }

        public bool CheckUserUniqueEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<UserDto> GetByEmail(string email)
        {
            return await _userRepository.GetByEmail(email);
        }

		public async Task<UserDto> GetUserById(int userId)
		{
			return await _userRepository.GetById(userId);
		}

		public async Task<(bool IsLoginSuccess, Tokens JwtTokens)> Login(LoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                return (false, null);
            }

            var user = await _userRepository.GetByEmail(loginDto.Email);

            if (user == null) { return (false, null); }

            bool validPassword = PasswordVerification(loginDto.Password, user.Password);
            if (!validPassword) { return (false, null); }

            var jwtTokens = await _jwtAuthenticationManagerService.GenerateJwtToken(user);
            //// update refresh token of logged in user
            //user.RefreshToken = jwtTokens.Refresh_Token;
            //await _unitOfWork.Repository<User>().UpdateAsync(user);
            //await _unitOfWork.Save();

            return (true, jwtTokens);
        }

        public async Task<(bool IsUserRegistered, string Message)> Register(UserRegistrationDto userRegistration)
        {
            var existedUser = await _userRepository.GetByEmail(userRegistration.Email);
            if (existedUser != null)
            {
                return (false, "Email Address  already registered. Please re-enter email address.");
            }
            var newUser = FromUserRegistrationModelToUserModel(userRegistration);
            await _unitOfWork.Repository<User>().AddAsync(newUser);
            await _unitOfWork.Save();
            await SaveDefaultRoleForNewUser(newUser);
            return (true, "Success");
        }

		public async Task<(bool IsUpdatedProfile, string Message)> UpdateProfile(UserRegistrationDto userRegistration)
		{
			var existedUser = await _userRepository.GetByEmail(userRegistration.Email);
			if (existedUser == null)
			{
				return (false, "Email does not exist. Update Profile Error.");
			}
            var user = new User()
            {
                Id = existedUser.Id,
                Email = userRegistration.Email,
                Password = userRegistration.Password,
                FirstName = userRegistration.FirstName,
                LastName = existedUser.LastName,
                Gender = userRegistration.Gender,
                DateOfBirth = userRegistration.DateOfBirth,
                Address = userRegistration.Address,
            };
            await _unitOfWork.Repository<User>().UpdateAsync(user);
			await _unitOfWork.Save();
			return (true, "Success");
		}

		public async Task<(bool IsSavedOrUpdatedUser, string Message)> SaveOrUpdateUser(UserDto userDto)
		{
			var user = new User
			{
                Id = userDto.Id,
				Email = userDto.Email,
				Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
				FirstName = userDto.FirstName,
				LastName = userDto.LastName,
				Gender = userDto.Gender,
				DateOfBirth = userDto.DateOfBirth,
				Address = userDto.Address
			};
            if (userDto.Id != 0)
                await _unitOfWork.Repository<User>().UpdateAsync(user);
            else
                await _unitOfWork.Repository<User>().AddAsync(user);

            await _unitOfWork.Save();
			if (userDto.Id == 0)
            {
				await SaveDefaultRoleForNewUser(user);
				await _unitOfWork.Save();
			}
				
			return (true, "Success");
		}

		#region Utilities of User
		private User FromUserRegistrationModelToUserModel(UserRegistrationDto userRegistration)
        {
            return new User
            {
                Email = userRegistration.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(userRegistration.Password),
                FirstName = userRegistration.FirstName,
                LastName = userRegistration.LastName,
                Gender = userRegistration.Gender,
                DateOfBirth = userRegistration.DateOfBirth,
                Address = userRegistration.Address,
            };
        }
        private bool PasswordVerification(string plainPassword, string dbPassword)
        {
            if (!BCrypt.Net.BCrypt.Verify(plainPassword, dbPassword))
            {
                return false;
            }
            return true;
        }
        private async Task SaveDefaultRoleForNewUser(User user)
        {
            var role = await _unitOfWork.Repository<Role>().Entities.FirstOrDefaultAsync(x => x.Id == (int)EnumRoles.User);
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id, // always User role for User Register
            };
            await _unitOfWork.Repository<UserRole>().AddAsync(userRole);
            await _unitOfWork.Save();
        }

		public async Task<bool> DeleteUser(int userId)
		{
			var user = await _unitOfWork.Repository<User>().Entities.FirstOrDefaultAsync(x => x.Id == userId && x.IsDeleted == false);
			if (user == null) { return false; }
            // just update IsDeleted = true;
            user.IsDeleted = true;
			await _unitOfWork.Repository<User>().UpdateAsync(user);
			await _unitOfWork.Save();
			return true;
		}
		#endregion
	}
}
