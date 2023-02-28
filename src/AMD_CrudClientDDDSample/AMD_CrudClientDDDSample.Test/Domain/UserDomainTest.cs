using AMD_CrudClientDDDSample.Domain.Command;
using AMD_CrudClientDDDSample.Domain.Command.User;
using AMD_CrudClientDDDSample.Domain.Entity;
using AMD_CrudClientDDDSample.Domain.Handlers;
using AMD_CrudClientDDDSample.Domain.Repository.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Messages;
using AMD_CrudClientDDDSample.Test.Repository;
using Flunt.Notifications;
using NSubstitute;
using Xunit;

namespace AMD_CrudClientDDDSample.Test.Domain
{
    public class UserDomainTest
    {
        private IUserRepository _mockUserRepository;
        private User _fakeUserEntity;
        private User _fakeUserPJEntity;
        private List<User> _listFakeUserEntity;

        public UserDomainTest()
        {
            _mockUserRepository = Substitute.For<IUserRepository>();
            _fakeUserEntity = FakeUserRepository.FakeUser(true);
            _fakeUserPJEntity = FakeUserRepository.FakeUser(false);
            _listFakeUserEntity = FakeUserRepository.ListFakeUser();
        }

        [Fact]
        public async Task CreateUserPFSucess()
        {
            // Arrange
            _mockUserRepository.Create(Arg.Any<User>()).Returns(_fakeUserEntity);
            var command = new CreateUserCommand("Fake", "Teste123", "13586155000");
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as GenericCommandResult)?.Success);
            Assert.Equal(_fakeUserEntity.Name, ((result as GenericCommandResult)?.Data as User)?.Name);
            Assert.Equal(_fakeUserEntity.Password, ((result as GenericCommandResult)?.Data as User)?.Password);
            Assert.Equal(_fakeUserEntity.CnpjCpf, ((result as GenericCommandResult)?.Data as User)?.CnpjCpf);
            Assert.True(((result as GenericCommandResult)?.Data as User)?.IsActive);
        }

        [Fact]
        public async Task CreateUserPJSucess()
        {
            // Arrange
            _mockUserRepository.Create(Arg.Any<User>()).Returns(_fakeUserPJEntity);
            var command = new CreateUserCommand("FakePJ", "Teste123", "67700676000169");
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as GenericCommandResult)?.Success);
            Assert.Equal(_fakeUserPJEntity.Name, ((result as GenericCommandResult)?.Data as User)?.Name);
            Assert.Equal(_fakeUserPJEntity.Password, ((result as GenericCommandResult)?.Data as User)?.Password);
            Assert.Equal(_fakeUserPJEntity.CnpjCpf, ((result as GenericCommandResult)?.Data as User)?.CnpjCpf);
            Assert.True(((result as GenericCommandResult)?.Data as User)?.IsActive);
        }

        [Fact]
        public async Task DeleteUserSucess()
        {
            // Arrange
            _mockUserRepository.GetById(Arg.Any<Guid>()).Returns(_fakeUserEntity);
            var command = new DeleteUserCommand(Guid.NewGuid());
            var handler = new UserHandler(_mockUserRepository);

            // Action
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as GenericCommandResult)?.Success);
            Assert.Equal(_fakeUserEntity.Name, ((result as GenericCommandResult)?.Data as User)?.Name);
            Assert.Equal(_fakeUserEntity.CnpjCpf, ((result as GenericCommandResult)?.Data as User)?.CnpjCpf);
            Assert.Equal(_fakeUserEntity.Password, ((result as GenericCommandResult)?.Data as User)?.Password);
            Assert.False(((result as GenericCommandResult)?.Data as User)?.IsActive);
        }

        [Fact]
        public async Task GetUserByNameSucess()
        {
            // Arrange
            _mockUserRepository.GetByField(Arg.Any<Dictionary<object, object>>()).Returns(
                _listFakeUserEntity.Where(c => c.Name.Equals("Fake")).ToList());
            var command = new GetByNameCnpjCpfUserCommand("Fake", string.Empty);
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert            
            Assert.NotNull(result);
            Assert.True((result as GenericCommandResult)?.Success);
            Assert.Equal(_fakeUserEntity.Name, ((result as GenericCommandResult)?.Data as List<User>)?[0].Name);
            Assert.Equal(_fakeUserEntity.CnpjCpf, ((result as GenericCommandResult)?.Data as List<User>)?[0].CnpjCpf);
            Assert.Equal(_fakeUserEntity.Password, ((result as GenericCommandResult)?.Data as List<User>)?[0].Password);
            Assert.True(((result as GenericCommandResult)?.Data as List<User>)?[0].IsActive);
        }

        [Fact]
        public async Task GetUserByNamePJSucess()
        {
            // Arrange
            _mockUserRepository.GetByField(Arg.Any<Dictionary<object, object>>()).Returns(
                _listFakeUserEntity.Where(c => c.Name.Equals("FakePJ")).ToList());
            var command = new GetByNameCnpjCpfUserCommand("FakePJ", string.Empty);
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert            
            Assert.NotNull(result);
            Assert.True((result as GenericCommandResult)?.Success);
            Assert.Equal(_fakeUserPJEntity.Name, ((result as GenericCommandResult)?.Data as List<User>)?[0].Name);
            Assert.Equal(_fakeUserPJEntity.CnpjCpf, ((result as GenericCommandResult)?.Data as List<User>)?[0].CnpjCpf);
            Assert.Equal(_fakeUserPJEntity.Password, ((result as GenericCommandResult)?.Data as List<User>)?[0].Password);
            Assert.True(((result as GenericCommandResult)?.Data as List<User>)?[0].IsActive);
        }

        [Fact]
        public async Task GetUserByCpfSucess()
        {
            // Arrange
            _mockUserRepository.GetByField(Arg.Any<Dictionary<object, object>>()).Returns(
                _listFakeUserEntity.Where(c => c.CnpjCpf.Equals("13586155000")).ToList());
            var command = new GetByNameCnpjCpfUserCommand(string.Empty, "13586155000");
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as GenericCommandResult)?.Success);
            Assert.Equal(_fakeUserEntity.Name, ((result as GenericCommandResult)?.Data as List<User>)?[0].Name);
            Assert.Equal(_fakeUserEntity.CnpjCpf, ((result as GenericCommandResult)?.Data as List<User>)?[0].CnpjCpf);
            Assert.Equal(_fakeUserEntity.Password, ((result as GenericCommandResult)?.Data as List<User>)?[0].Password);
            Assert.True(((result as GenericCommandResult)?.Data as List<User>)?[0].IsActive);
        }

        [Fact]
        public async Task GetUserByCnpjSucess()
        {
            // Arrange
            _mockUserRepository.GetByField(Arg.Any<Dictionary<object, object>>()).Returns(
                _listFakeUserEntity.Where(c => c.CnpjCpf.Equals("67700676000169")).ToList());
            var command = new GetByNameCnpjCpfUserCommand(string.Empty, "67700676000169");
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as GenericCommandResult)?.Success);
            Assert.Equal(_fakeUserPJEntity.Name, ((result as GenericCommandResult)?.Data as List<User>)?[0].Name);
            Assert.Equal(_fakeUserPJEntity.CnpjCpf, ((result as GenericCommandResult)?.Data as List<User>)?[0].CnpjCpf);
            Assert.Equal(_fakeUserPJEntity.Password, ((result as GenericCommandResult)?.Data as List<User>)?[0].Password);
            Assert.True(((result as GenericCommandResult)?.Data as List<User>)?[0].IsActive);
        }

        [Fact]
        public async Task GetUserByIdSucess()
        {
            // Arrange
            _mockUserRepository.GetById(Arg.Any<Guid>()).Returns(_fakeUserEntity);
            var command = new GetByIdUserCommand(Guid.NewGuid());
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as GenericCommandResult)?.Success);
            Assert.Equal(((result as GenericCommandResult)?.Data as User)?.Name, _fakeUserEntity.Name);
            Assert.Equal(((result as GenericCommandResult)?.Data as User)?.CnpjCpf, _fakeUserEntity.CnpjCpf);
            Assert.Equal(((result as GenericCommandResult)?.Data as User)?.Password, _fakeUserEntity.Password);
            Assert.True(((result as GenericCommandResult)?.Data as User)?.IsActive);
        }

        [Fact]
        public async Task CreateUserNameCnpjCpfPasswordNull()
        {
            // Arrange
            var command = new CreateUserCommand(string.Empty, string.Empty, string.Empty);
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UserMessage.UserNameNotInforming, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
            Assert.Equal(UserMessage.NameMaxCharacter, ((result as GenericCommandResult)?.Data as List<Notification>)?[1].Message);
            Assert.Equal(UserMessage.PasswordMinCharacter, ((result as GenericCommandResult)?.Data as List<Notification>)?[2].Message);
            Assert.Equal(UserMessage.CnpjCpfMaxCharacter, ((result as GenericCommandResult)?.Data as List<Notification>)?[3].Message);
            Assert.Equal(UserMessage.CnpjCpfInvalid, ((result as GenericCommandResult)?.Data as List<Notification>)?[4].Message);
        }

        [Fact]
        public async Task CreateUserNameInvalid()
        {
            // Arrange
            var command = new CreateUserCommand("T", "teste1234","13586155000");
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UserMessage.UserNameNotInforming, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);

        }

        [Fact]
        public async Task CreateUsertName35Characters()
        {
            // Arrange
            var command = new CreateUserCommand("tttttttttttttttttttttttttttttttttt",
                "teste1234","13586155000");
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UserMessage.NameMaxCharacter, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);

        }

        [Fact]
        public async Task CreateUserCpfInvalid()
        {
            // Arrange
            var command = new CreateUserCommand("Fake", "Teste1234" ,"12345678901");
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UserMessage.CnpjCpfInvalid, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task CreateUserCnpjInvalid()
        {
            // Arrange
            var command = new CreateUserCommand("Fake", "Teste1234", "12345678000191");
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UserMessage.CnpjCpfInvalid, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task CreateUserPassword5Characters()
        {
            // Arrange
            var command = new CreateUserCommand("Fake", "teste" , "13586155000");
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UserMessage.PasswordMinCharacter, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task DeleteUserNotFound()
        {
            // Arrange
            _mockUserRepository.GetById(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")).Returns(_fakeUserEntity);
            var command = new DeleteUserCommand(Guid.NewGuid());
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.False((result as GenericCommandResult)?.Success);
            Assert.Equal(UserMessage.UserNotFound, (result as GenericCommandResult)?.Message);
        }

        [Fact]
        public async Task DeleteUserIdInvalid()
        {
            // Arrange
            var command = new DeleteUserCommand(Guid.Empty);
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UserMessage.IdInvalid, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task GetUserNameCnpjCpfError()
        {
            // Arrange
            var command = new GetByNameCnpjCpfUserCommand("Teste", "11111111111");
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UserMessage.NameOrCnpjCpf, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task GetUserNameCnpjCpfEmpty()
        {
            // Arrange
            var command = new GetByNameCnpjCpfUserCommand(string.Empty, string.Empty);
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UserMessage.NameOrCnpjCpf, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task GetUserCpfInvalid()
        {
            // Arrange
            var command = new GetByNameCnpjCpfUserCommand(string.Empty, "11111111111");
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UserMessage.CnpjCpfInvalid, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task GetUserCnpjInvalid()
        {
            // Arrange
            var command = new GetByNameCnpjCpfUserCommand(string.Empty, "11111111000111");
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UserMessage.CnpjCpfInvalid, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task GetUserIdInvalid()
        {
            // Arrange
            var command = new GetByIdUserCommand(Guid.Empty);
            var handler = new UserHandler(_mockUserRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UserMessage.IdInvalid, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }
    }
}