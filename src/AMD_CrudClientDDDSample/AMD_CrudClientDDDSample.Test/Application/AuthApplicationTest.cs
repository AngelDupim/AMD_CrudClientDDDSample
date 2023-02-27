using AMD_CrudClientDDDSample.Application.Command;
using AMD_CrudClientDDDSample.Application.Command.Auth;
using AMD_CrudClientDDDSample.Application.Handler;
using AMD_CrudClientDDDSample.Application.Mapper;
using AMD_CrudClientDDDSample.Domain.Command;
using AMD_CrudClientDDDSample.Domain.Command.Auth;
using AMD_CrudClientDDDSample.Domain.Entity;
using AMD_CrudClientDDDSample.Domain.Handlers.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Messages;
using AMD_CrudClientDDDSample.Test.Fake;
using AutoMapper;
using Flunt.Notifications;
using NSubstitute;
using Xunit;

namespace AMD_CrudClientDDDSample.Test.Application
{
    public class AuthApplicationTest
    {
        private IAuthHandler _mockHandle;
        private IMapper _mapperConfig;
        private Auth _fakeAuthEntity;

        public AuthApplicationTest()
        {
            _mockHandle = Substitute.For<IAuthHandler>();
            _mapperConfig = MapperConfig.RegisterMapper();
            _fakeAuthEntity = AuthFake.TokenSucess();
        }

        [Fact]
        public async Task GetAuthSucess()
        {
            // Arrange
            var commandResult = new GenericCommandResult(true, UserMessage.GetSucess, _fakeAuthEntity);
            _mockHandle.Handle(Arg.Any<GetAuthCommand>()).Returns(commandResult);
            var application = new AuthHandlerApplication(_mapperConfig, _mockHandle);
            var command = new GetAuthCommandApplication("Fake", "Teste123");

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as CommandResultModel)?.Success);
            Assert.True(((result as CommandResultModel)?.Data as Auth)?.Authenticated);
            Assert.Equal(_fakeAuthEntity.AccessToken, ((result as CommandResultModel)?.Data as Auth)?.AccessToken);
            Assert.Equal(_fakeAuthEntity.Created, ((result as CommandResultModel)?.Data as Auth)?.Created);
            Assert.Equal(_fakeAuthEntity.Expiration, ((result as CommandResultModel)?.Data as Auth)?.Expiration);
            Assert.Equal(_fakeAuthEntity.Message, ((result as CommandResultModel)?.Data as Auth)?.Message);
        }

        [Fact]
        public async Task GetAuthErroUserEmpty()
        {
            // Arrange
            var notifications = new List<Notification> {
                new Notification("Name", AuthMessage.UserNotInformed)
            };

            var commandResult = new GenericCommandResult(false, AuthMessage.UserNotInformed, notifications);
            _mockHandle.Handle(Arg.Any<GetAuthCommand>()).Returns(commandResult);
            var application = new AuthHandlerApplication(_mapperConfig, _mockHandle);
            var command = new GetAuthCommandApplication(string.Empty, "Teste123");

            // Act
            var result = await application.Handle(command);

            // Assert            
            Assert.NotNull(result);
            Assert.Equal(AuthMessage.UserNotInformed, ((result as CommandResultModel)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task GetAuthErroPasswordEmpty()
        {
            // Arrange
            var notifications = new List<Notification> {
                new Notification("Password", AuthMessage.PasswordNotInformed)
            };

            var commandResult = new GenericCommandResult(false, AuthMessage.PasswordNotInformed, notifications);
            _mockHandle.Handle(Arg.Any<GetAuthCommand>()).Returns(commandResult);
            var application = new AuthHandlerApplication(_mapperConfig, _mockHandle);
            var command = new GetAuthCommandApplication("Fake", string.Empty);

            // Act
            var result = await application.Handle(command);

            // Assert            
            Assert.NotNull(result);
            Assert.Equal(AuthMessage.PasswordNotInformed, ((result as CommandResultModel)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task GetAuthErroNotFound()
        {
            // Arrange
            var commandResult = new GenericCommandResult(false, AuthMessage.UserOrPasswordError, null);
            _mockHandle.Handle(Arg.Any<GetAuthCommand>()).Returns(commandResult);
            var application = new AuthHandlerApplication(_mapperConfig, _mockHandle);
            var command = new GetAuthCommandApplication("Fake", "teste123");

            // Act
            var result = await application.Handle(command);

            // Assert            
            Assert.NotNull(result);
            Assert.Equal(AuthMessage.UserOrPasswordError, (result as CommandResultModel)?.Message);
        }
    }
}