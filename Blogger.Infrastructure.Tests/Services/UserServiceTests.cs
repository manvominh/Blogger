using AutoFixture;
using AutoFixture.AutoMoq;
using Blogger.Application.Interfaces.Repositories;
using Blogger.Application.Interfaces.Services;
using Blogger.Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Blogger.Infrastructure.Tests.Services
{
	public class UserServiceTests
	{
		private readonly IFixture _fixture;
		private readonly Mock<IUserRepository> _userRepositoryMock;
		private readonly Mock<IUnitOfWork> _unitOfWorkMock;
		private readonly Mock<IJwtAuthenticationManagerService> _jwtAuthenticationManagerService;
		private readonly UserService _sut;
		public UserServiceTests()
		{
			_fixture = new Fixture();
			_fixture.Customize(new AutoMoqCustomization()); // use Customize to avoid error in calling AutoFixture over 2 times

			_userRepositoryMock = _fixture.Freeze<Mock<IUserRepository>>();
			_unitOfWorkMock = _fixture.Freeze<Mock<IUnitOfWork>>();
			_jwtAuthenticationManagerService = _fixture.Freeze<Mock<IJwtAuthenticationManagerService>>();
			_sut = new UserService(_userRepositoryMock.Object, _unitOfWorkMock.Object, _jwtAuthenticationManagerService.Object);
		}
		[Fact]
		public void UsersServiceConstructor_ShouldReturnNullReferenceException_WhenUserRepositoryIsNull()
		{
			// Arrange
			IUserRepository userRepository = null;

			// Act && Assert
			Assert.Throws<ArgumentNullException>(() => new UserService(userRepository, _unitOfWorkMock.Object, _jwtAuthenticationManagerService.Object));
		}

		[Fact]
		public void UsersServiceConstructor_ShouldReturnNullReferenceException_WhenUnitOfWorkIsNull()
		{
			// Arrange            
			IUnitOfWork unitOfWork = null;

			// Act && Assert
			Assert.Throws<ArgumentNullException>(() => new UserService(_userRepositoryMock.Object, unitOfWork, _jwtAuthenticationManagerService.Object));
		}
		[Fact]
		public void UsersServiceConstructor_ShouldReturnNullReferenceException_WhenJwtAuthenticationManagerServiceIsNull()
		{
			// Arrange            
			IJwtAuthenticationManagerService jwtAuthenticationManagerService = null;

			// Act && Assert
			Assert.Throws<ArgumentNullException>(() => new UserService(_userRepositoryMock.Object, _unitOfWorkMock.Object, jwtAuthenticationManagerService));
		}
	}
}
