using AutoFixture;
using Azure.Core;
using Blogger.API.Controllers;
using Blogger.Application.Dtos;
using Blogger.Application.Interfaces.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using Xunit.Sdk;

namespace BloggerController.Tests.Controllers
{
    public class UsersControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUserService> _serviceMock;
        private readonly UsersController _sut;
        public UsersControllerTest()
        {
            _fixture = new Fixture();
			_fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
			_fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

			_serviceMock = _fixture.Freeze<Mock<IUserService>>();
            _sut = new UsersController(_serviceMock.Object);//creates the implementation in-memory
        }
        [Fact]
        public void UsersControllerConstructor_ShouldReturnNullReferenceException_WhenServiceIsNull()
        {
            // Arrange
            IUserService userService = null;

            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => new UsersController(userService));
        }
        [Fact]
        public async Task UsersController_GetAllUsers_ShouldReturnOkResponse_WhenDataFound()
        {
            // Arrange
            var usersMock = _fixture.Create<IEnumerable<UserDto>>();
            _serviceMock.Setup(x => x.GetAll()).ReturnsAsync(usersMock);

            // Act
            var result = await _sut.GetAllUsers().ConfigureAwait(false);

            // Assert
            Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
			//result.Result.Should().BeAssignableTo<OkResult>(); //Technically there is no difference between the two approaches.
			result.Should().BeAssignableTo<OkObjectResult>();
			result.As<OkObjectResult>().Value
				.Should()
                .NotBeNull()
                .And.BeOfType(usersMock.GetType());
            _serviceMock.Verify(x => x.GetAll(), Times.Once());
        }
		[Fact]
		public async Task UsersController_GetUsers_ShouldReturnNotFound_WhenDataNotFound()
		{
			// Arrange
			List<UserDto> response = null;
			_serviceMock.Setup(x => x.GetAll()).ReturnsAsync(response);

			// Act
			var result = await _sut.GetAllUsers().ConfigureAwait(false);

			// Assert
			result.Should().NotBeNull();
			result.Should().BeAssignableTo<NotFoundResult>();
			_serviceMock.Verify(x => x.GetAll(), Times.Once());
		}
		[Fact]
		public async Task UsersController_Register_ShouldReturnOkResponse_WhenSuccess()
		{
			// Arrange
			var request = _fixture.Create<UserRegistrationDto>();
			var response = (IsUserRegistered: true, Message: "success");
			_serviceMock.Setup(x => x.Register(request)).ReturnsAsync(response);

			// Act
			var result = await _sut.Register(request).ConfigureAwait(false);

			// Assert
			result.Should().NotBeNull();
			result.Should().BeAssignableTo<IActionResult>();
			result.Should().BeOfType<OkObjectResult>();
			result.As<OkObjectResult>().Value.Should().Be(response.Message);
			_serviceMock.Verify(x => x.Register(request), Times.Once());
		}
		[Fact]
		public async Task UsersController_Register_ShouldReturnBadRequest_WhenFailed()
		{
			// Arrange
			var request = _fixture.Create<UserRegistrationDto>();
			var response = (IsUserRegistered: false, Message:"Error");
			_serviceMock.Setup(x => x.Register(request)).ReturnsAsync(response);
			// Act
			var result = await _sut.Register(request).ConfigureAwait(false);

			// Assert
			result.Should().NotBeNull();
			result.Should().BeAssignableTo<IActionResult>();
			result.Should().BeOfType<BadRequestObjectResult>();
			result.As<BadRequestObjectResult>().Value.Should().NotBeNull();
			_serviceMock.Verify(x => x.Register(request), Times.Once());
		}
		[Fact]
		public async Task UsersController_Login_ShouldReturnOkResponse_WhenSuccess()
		{
			// Arrange
			var request = _fixture.Create<LoginDto>();
			var tokens = _fixture.Create<Tokens>();
			var response = (IsLoginSuccess: true, tokens);
			_serviceMock.Setup(x => x.Login(request)).ReturnsAsync(response);

			// Act
			var result = await _sut.Login(request).ConfigureAwait(false);

			// Assert
			result.Should().NotBeNull();
			result.Should().BeAssignableTo<IActionResult>();
			result.Should().BeOfType<OkObjectResult>();
			result.As<OkObjectResult>().Value.Should().Be(response.tokens);
			_serviceMock.Verify(x => x.Login(request), Times.Once());
		}
		[Fact]
		public async Task UsersController_Login_ShouldReturnBadRequest_WhenFailed()
		{
			// Arrange
			var request = _fixture.Create<LoginDto>();
			var tokens = _fixture.Create<Tokens>();
			var response = (IsLoginSuccess: false, tokens);
			_serviceMock.Setup(x => x.Login(request)).ReturnsAsync(response);
			// Act
			var result = await _sut.Login(request).ConfigureAwait(false);

			// Assert
			result.Should().NotBeNull();
			result.Should().BeAssignableTo<IActionResult>();
			result.Should().BeOfType<BadRequestObjectResult>();
			result.As<BadRequestObjectResult>().Value.Should().NotBeNull();
			_serviceMock.Verify(x => x.Login(request), Times.Once());
		}
		[Fact]
		public async Task UsersController_GetUserById_ShouldReturnOkResponse_WhenValidInput()
		{
			// Arrange
			var userMock = _fixture.Create<UserDto>();
			var id = _fixture.Create<int>();
			_serviceMock.Setup(x => x.GetUserById(id)).ReturnsAsync(userMock);

			// Act
			var result = await _sut.GetUserById(id).ConfigureAwait(false);

			// Assert
			result.Should().NotBeNull();
			result.Should().BeAssignableTo<IActionResult>();
			result.Should().BeAssignableTo<OkObjectResult>();
			result.As<OkObjectResult>().Value
				.Should()
				.NotBeNull()
				.And.BeOfType(userMock.GetType());
			_serviceMock.Verify(x => x.GetUserById(id), Times.Once());
		}
		[Fact]
		public async Task UsersController_GetUserById_ShouldReturnBadRequest_WhenNoDataFound()
		{
			// Arrange
			UserDto response = null;
			var id = _fixture.Create<int>();
			_serviceMock.Setup(x => x.GetUserById(id)).ReturnsAsync(response);

			// Act
			var result = await _sut.GetUserById(id).ConfigureAwait(false);

			// Assert
			result.Should().NotBeNull();
			result.Should().BeAssignableTo<IActionResult>();
			result.Should().BeOfType<BadRequestObjectResult>();
			result.As<BadRequestObjectResult>().Value.Should().NotBeNull();
			_serviceMock.Verify(x => x.GetUserById(id), Times.Once());
		}
		[Fact]
		public async Task UsersController_GetUserByEmail_ShouldReturnOkResponse_WhenValidInput()
		{
			// Arrange
			var userMock = _fixture.Create<UserDto>();
			var email = _fixture.Create<string>();
			_serviceMock.Setup(x => x.GetUserByEmail(email)).ReturnsAsync(userMock);

			// Act
			var result = await _sut.GetUserByEmail(email).ConfigureAwait(false);

			// Assert
			result.Should().NotBeNull();
			result.Should().BeAssignableTo<IActionResult>();
			result.Should().BeAssignableTo<OkObjectResult>();
			result.As<OkObjectResult>().Value
				.Should()
				.NotBeNull()
				.And.BeOfType(userMock.GetType());
			_serviceMock.Verify(x => x.GetUserByEmail(email), Times.Once());
		}
		[Fact]
		public async Task UsersController_GetUserByEmail_ShouldReturnBadRequest_WhenNoDataFound()
		{
			// Arrange
			UserDto response = null;
			var email = _fixture.Create<string>();
			_serviceMock.Setup(x => x.GetUserByEmail(email)).ReturnsAsync(response);

			// Act
			var result = await _sut.GetUserByEmail(email).ConfigureAwait(false);

			// Assert
			result.Should().NotBeNull();
			result.Should().BeAssignableTo<IActionResult>();
			result.Should().BeOfType<BadRequestObjectResult>();
			result.As<BadRequestObjectResult>().Value.Should().NotBeNull();
			_serviceMock.Verify(x => x.GetUserByEmail(email), Times.Once());
		}
	}
}
