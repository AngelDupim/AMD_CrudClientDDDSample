using AMD_CrudClientDDDSample.Application.Command.User;
using AMD_CrudClientDDDSample.Application.Command;
using AMD_CrudClientDDDSample.Domain.Entity;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Messages;
using AMD_CrudClientDDDSample.Services.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;
using AMD_CrudClientDDDSample.Application.Command.Auth;
using AMD_CrudClientDDDSample.Application.Handler.Interfaces;
using AMD_CrudClientDDDSample.Test.Fake;
using NSubstitute.ExceptionExtensions;

namespace AMD_CrudClientDDDSample.Test.Services
{
    public class AuthServericesTeste
    {

        private IAuthHandlerApplication _mockHandle;
        private Auth _fakeAuthEntity;
        public AuthServericesTeste()
        {
            _mockHandle = Substitute.For<IAuthHandlerApplication>();
            _fakeAuthEntity = AuthFake.TokenSucess();
        }

        [Fact]
        public async Task CreateTokenSucess()
        {
            // Arrange
            var commandResult = new CommandResultModel(true, AuthMessage.AuthSucess, _fakeAuthEntity);
            var command = new GetAuthCommandApplication("Fake","teste123");
            _mockHandle.Handle(Arg.Any<GetAuthCommandApplication>()).Returns(commandResult);
            var controller = new AuthController(_mockHandle);

            // Act
            var result = await controller.CreateToken(command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(AuthMessage.AuthSucess, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(_fakeAuthEntity.AccessToken, (((result?.Value as CommandResultModel)?.Data as Auth)?.AccessToken));
            Assert.Equal(_fakeAuthEntity.Expiration, (((result?.Value as CommandResultModel)?.Data as Auth)?.Expiration));
            Assert.Equal(_fakeAuthEntity.Created, (((result?.Value as CommandResultModel)?.Data as Auth)?.Created));
            Assert.True((((result?.Value as CommandResultModel)?.Data as Auth)?.Authenticated));
        }

        [Fact]
        public async Task CreateTokenErroExceptionController()
        {
            // Arrange
            var command = new GetAuthCommandApplication(string.Empty, string.Empty);
            _mockHandle.Handle(Arg.Any<GetAuthCommandApplication>()).Throws(new Exception(AuthMessage.AuthError));
            var controller = new AuthController(_mockHandle);

            // Act
            var result = await controller.CreateToken(command) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.False((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(AuthMessage.AuthError, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(AuthMessage.AuthError, (result?.Value as CommandResultModel)?.Data);
        }

    }
}