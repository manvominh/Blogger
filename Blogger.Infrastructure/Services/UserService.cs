using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Repositories;
using Blogger.Application.Interfaces.Services;
using Blogger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _userRepository.GetByEmail(email);
        }

        public async Task<(bool IsLoginSuccess, UserSession? UserSession)> Login(LoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                return (false, null);
            }

            var user = await _userRepository.GetByEmail(loginDto.Email);

            if (user == null) { return (false, null); }

            bool validPassword = PasswordVerification(loginDto.Password, user.Password);
            if (!validPassword) { return (false, null); }

            var userSession = await _jwtAuthenticationManagerService.GenerateJwtToken(user);
           
            return (true, userSession);
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
            var role = await _unitOfWork.Repository<Role>().Entities.FirstOrDefaultAsync(x => x.Id == 2);
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id, // always User role for User Register
            };
            await _unitOfWork.Repository<UserRole>().AddAsync(userRole);
            await _unitOfWork.Save();
        }
        #endregion
    }
}
