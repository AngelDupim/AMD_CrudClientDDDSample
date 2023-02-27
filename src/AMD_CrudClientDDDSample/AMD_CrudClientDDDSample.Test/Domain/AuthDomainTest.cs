using AMD_CrudClientDDDSample.Domain.Command;
using AMD_CrudClientDDDSample.Domain.Entity;
using AMD_CrudClientDDDSample.Domain.Handlers;
using AMD_CrudClientDDDSample.Domain.Repository.Interfaces;
using AMD_CrudClientDDDSample.Test.Repository;
using NSubstitute;
using Xunit;
using AMD_CrudClientDDDSample.Domain.Command.Auth;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Messages;
using Flunt.Notifications;

namespace AMD_CrudClientDDDSample.Test.Domain
{
    public class AuthDomainTest
    {
        private IUserRepository _mockUserRepository;
        private List<User> _listFakeUserEntity;

        public AuthDomainTest()
        {
            _mockUserRepository = Substitute.For<IUserRepository>();
            _listFakeUserEntity = FakeUserRepository.ListFakeUser();
        }

        [Fact]
        public async Task GetAuthSucess()
        {
            // Arrange
            _mockUserRepository.GetByField(Arg.Any<Dictionary<object, object>>()).Returns(
                _listFakeUserEntity.Where(c => c.Name.Equals("Fake")).ToList());
            var command = new GetAuthCommand("Fake", "Teste123");
            var handler = new AuthHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert            
            Assert.NotNull(result);
            Assert.True((result as GenericCommandResult)?.Success);
            Assert.True(((result as GenericCommandResult)?.Data as Auth)?.Authenticated);
            Assert.NotNull(((result as GenericCommandResult)?.Data as Auth)?.AccessToken);
            Assert.NotNull(((result as GenericCommandResult)?.Data as Auth)?.Created);
            Assert.NotNull(((result as GenericCommandResult)?.Data as Auth)?.Expiration);
            Assert.NotNull(((result as GenericCommandResult)?.Data as Auth)?.Message);
        }

        [Fact]
        public async Task GetAuthErroUserEmpty()
        {
            // Arrange
            _mockUserRepository.GetByField(Arg.Any<Dictionary<object, object>>()).Returns(
                _listFakeUserEntity.Where(c => c.Name.Equals("Fake")).ToList());
            var command = new GetAuthCommand(string.Empty, "Teste123");
            var handler = new AuthHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert            
            Assert.NotNull(result);
            Assert.Equal(AuthMessage.UserNotInformed, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task GetAuthErroPasswordEmpty()
        {
            // Arrange
            _mockUserRepository.GetByField(Arg.Any<Dictionary<object, object>>()).Returns(
                _listFakeUserEntity.Where(c => c.Name.Equals("Fake")).ToList());
            var command = new GetAuthCommand("Fake", string.Empty);
            var handler = new AuthHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert            
            Assert.NotNull(result);
            Assert.Equal(AuthMessage.PasswordNotInformed, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task GetAuthErroPasswordInvalid()
        {
            // Arrange
            _mockUserRepository.GetByField(Arg.Any<Dictionary<object, object>>()).Returns(
                _listFakeUserEntity.Where(c => c.Name.Equals("Fake")).ToList());
            var command = new GetAuthCommand("Fake", "Teste1234");
            var handler = new AuthHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert            
            Assert.NotNull(result);
            Assert.Equal(AuthMessage.UserOrPasswordError, (result as GenericCommandResult)?.Message);
        }

        [Fact]
        public async Task GetAuthErroNotFound()
        {
            // Arrange
            var command = new GetAuthCommand("Fake", "Teste123");
            var handler = new AuthHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert            
            Assert.NotNull(result);
            Assert.Equal(AuthMessage.UserOrPasswordError, (result as GenericCommandResult)?.Message);
        }
    }
}