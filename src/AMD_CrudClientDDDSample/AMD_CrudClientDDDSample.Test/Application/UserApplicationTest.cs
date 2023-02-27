using AMD_CrudClientDDDSample.Application.Command;
using AMD_CrudClientDDDSample.Application.Command.User;
using AMD_CrudClientDDDSample.Application.Handler;
using AMD_CrudClientDDDSample.Application.Mapper;
using AMD_CrudClientDDDSample.Domain.Command;
using AMD_CrudClientDDDSample.Domain.Command.User;
using AMD_CrudClientDDDSample.Domain.Entity;
using AMD_CrudClientDDDSample.Domain.Handlers.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Messages;
using AMD_CrudClientDDDSample.Test.Repository;
using AutoMapper;
using Flunt.Notifications;
using NSubstitute;
using Xunit;

namespace AMD_CrudClientDDDSample.Test.Application
{
    public class UserApplicationTest
    {
        private User _fakeUserEntity;
        private User _fakeUserPJEntity;
        private List<User> _listFakeUserEntity;
        private IUserHandler _mockHandle;
        private IMapper _mapperConfig;

        public UserApplicationTest()
        {
            _mockHandle = Substitute.For<IUserHandler>();
            _fakeUserEntity = FakeUserRepository.FakeUser(true);
            _fakeUserPJEntity = FakeUserRepository.FakeUser(false);
            _listFakeUserEntity = FakeUserRepository.ListFakeUser();
            _mapperConfig = MapperConfig.RegisterMapper();
        }

        [Fact]
        public async Task CreateUserPFSucess()
        {
            // Arrange
            var commandResult = new GenericCommandResult(true, UserMessage.CreateSucess, _fakeUserEntity);
            _mockHandle.Handle(Arg.Any<CreateUserCommand>()).Returns(commandResult);
            var application = new UserHandlerApplication(_mapperConfig, _mockHandle);
            var command = new CreateUserCommandApplication("Fake", "Teste123", "13586155000");

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.CreateSucess, (result as CommandResultModel)?.Message);
            Assert.Equal(_fakeUserEntity.Name, ((result as CommandResultModel)?.Data as User)?.Name);
            Assert.Equal(_fakeUserEntity.CnpjCpf, ((result as CommandResultModel)?.Data as User)?.CnpjCpf);
            Assert.Equal(_fakeUserEntity.Password, ((result as CommandResultModel)?.Data as User)?.Password);
            Assert.True(((result as CommandResultModel)?.Data as User)?.IsActive);
        }

        [Fact]
        public async Task CreateUserPJSucess()
        {
            // Arrange
            var commandResult = new GenericCommandResult(true, UserMessage.CreateSucess, _fakeUserPJEntity);
            _mockHandle.Handle(Arg.Any<CreateUserCommand>()).Returns(commandResult);
            var application = new UserHandlerApplication(_mapperConfig, _mockHandle);
            var command = new CreateUserCommandApplication("FakePJ", "Teste123","67700676000169");

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.CreateSucess, (result as CommandResultModel)?.Message);
            Assert.Equal(_fakeUserPJEntity.Name, ((result as CommandResultModel)?.Data as User)?.Name);
            Assert.Equal(_fakeUserPJEntity.CnpjCpf, ((result as CommandResultModel)?.Data as User)?.CnpjCpf);
            Assert.Equal(_fakeUserPJEntity.Password, ((result as CommandResultModel)?.Data as User)?.Password);
            Assert.True(((result as CommandResultModel)?.Data as User)?.IsActive);
        }

        [Fact]
        public async Task DeleteUserSucess()
        {
            // Arrange
            var commandResult = new GenericCommandResult(true, UserMessage.DeleteSucess, _fakeUserEntity);
            _mockHandle.Handle(Arg.Any<DeleteUserCommand>()).Returns(commandResult);
            var application = new UserHandlerApplication(_mapperConfig, _mockHandle);
            var command = new DeleteUserCommandApplication(Guid.NewGuid());

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.DeleteSucess, (result as CommandResultModel)?.Message);
            Assert.Equal(_fakeUserEntity.Name, ((result as CommandResultModel)?.Data as User)?.Name);
            Assert.Equal(_fakeUserEntity.CnpjCpf, ((result as CommandResultModel)?.Data as User)?.CnpjCpf);
            Assert.Equal(_fakeUserEntity.Password, ((result as CommandResultModel)?.Data as User)?.Password);
        }

        [Fact]
        public async Task GetByNamePFSucess()
        {
            // Arrange
            var commandResult = new GenericCommandResult(true, UserMessage.GetSucess,
                _listFakeUserEntity.Where(c => c.Name.Equals("Fake")).ToList());
            _mockHandle.Handle(Arg.Any<GetByNameCnpjCpfUserCommand>()).Returns(commandResult);
            var application = new UserHandlerApplication(_mapperConfig, _mockHandle);
            var commad = new GetByNameCnpjCpfUserCommandApplication("Fake", string.Empty);

            // Act
            var result = await application.Handle(commad);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.GetSucess, (result as CommandResultModel)?.Message);
            Assert.Equal(_fakeUserEntity.Name, ((result as CommandResultModel)?.Data as List<User>)?[0].Name);
            Assert.Equal(_fakeUserEntity.CnpjCpf, ((result as CommandResultModel)?.Data as List<User>)?[0].CnpjCpf);
            Assert.Equal(_fakeUserEntity.Password, ((result as CommandResultModel)?.Data as List<User>)?[0].Password);
            Assert.True(((result as CommandResultModel)?.Data as List<User>)?[0].IsActive);

        }

        [Fact]
        public async Task GetByNamePJSucess()
        {
            // Arrange
            var commandResult = new GenericCommandResult(true, UserMessage.GetSucess,
                _listFakeUserEntity.Where(c => c.Name.Equals("FakePJ")).ToList());
            _mockHandle.Handle(Arg.Any<GetByNameCnpjCpfUserCommand>()).Returns(commandResult);
            var application = new UserHandlerApplication(_mapperConfig, _mockHandle);
            var command = new GetByNameCnpjCpfUserCommandApplication("FakePJ", string.Empty);

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.GetSucess, (result as CommandResultModel)?.Message);
            Assert.Equal(_fakeUserPJEntity.Name, ((result as CommandResultModel)?.Data as List<User>)?[0].Name);
            Assert.Equal(_fakeUserPJEntity.CnpjCpf, ((result as CommandResultModel)?.Data as List<User>)?[0].CnpjCpf);
            Assert.Equal(_fakeUserPJEntity.Password, ((result as CommandResultModel)?.Data as List<User>)?[0].Password);
            Assert.True(((result as CommandResultModel)?.Data as List<User>)?[0].IsActive);
            
        }

        [Fact]
        public async Task GetByCpfSucess()
        {
            // Arrange
            var commandResult = new GenericCommandResult(true, UserMessage.GetSucess,
                _listFakeUserEntity.Where(c => c.CnpjCpf.Equals("13586155000")).ToList());
            _mockHandle.Handle(Arg.Any<GetByNameCnpjCpfUserCommand>()).Returns(commandResult);
            var application = new UserHandlerApplication(_mapperConfig, _mockHandle);
            var command = new GetByNameCnpjCpfUserCommandApplication(string.Empty, "13586155000");

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.GetSucess, (result as CommandResultModel)?.Message);
            Assert.Equal(_fakeUserEntity.Name, ((result as CommandResultModel)?.Data as List<User>)?[0].Name);
            Assert.Equal(_fakeUserEntity.CnpjCpf, ((result as CommandResultModel)?.Data as List<User>)?[0].CnpjCpf);
            Assert.Equal(_fakeUserEntity.Password, ((result as CommandResultModel)?.Data as List<User>)?[0].Password);
            Assert.True(((result as CommandResultModel)?.Data as List<User>)?[0].IsActive);

        }

        [Fact]
        public async Task GetByCnpjSucess()
        {
            // Arrange
            var commandResult = new GenericCommandResult(true, UserMessage.GetSucess,
                _listFakeUserEntity.Where(c => c.CnpjCpf.Equals("67700676000169")).ToList());
            _mockHandle.Handle(Arg.Any<GetByNameCnpjCpfUserCommand>()).Returns(commandResult);
            var application = new UserHandlerApplication(_mapperConfig, _mockHandle);
            var command = new GetByNameCnpjCpfUserCommandApplication(string.Empty, "67700676000169");

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.GetSucess, (result as CommandResultModel)?.Message);
            Assert.Equal(_fakeUserPJEntity.Name, ((result as CommandResultModel)?.Data as List<User>)?[0].Name);
            Assert.Equal(_fakeUserPJEntity.CnpjCpf, ((result as CommandResultModel)?.Data as List<User>)?[0].CnpjCpf);
            Assert.Equal(_fakeUserPJEntity.Password, ((result as CommandResultModel)?.Data as List<User>)?[0].Password);
            Assert.True(((result as CommandResultModel)?.Data as List<User>)?[0].IsActive);
        }

        [Fact]
        public async Task GetUserByIdSucess()
        {
            // Arrange
            var commandResult = new GenericCommandResult(true, UserMessage.GetSucess, _fakeUserEntity);
            _mockHandle.Handle(Arg.Any<GetByIdUserCommand>()).Returns(commandResult);
            var application = new UserHandlerApplication(_mapperConfig, _mockHandle);
            var commad = new GetByIdUserCommandApplication(Guid.NewGuid());

            // Act
            var result = await application.Handle(commad);

            Assert.NotNull(result);
            Assert.True((result as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.GetSucess, (result as CommandResultModel)?.Message);
            Assert.Equal(_fakeUserEntity.Name, ((result as CommandResultModel)?.Data as User)?.Name);
            Assert.Equal(_fakeUserEntity.CnpjCpf, ((result as CommandResultModel)?.Data as User)?.CnpjCpf);
            Assert.Equal(_fakeUserEntity.Password, ((result as CommandResultModel)?.Data as User)?.Password);
            Assert.True(((result as CommandResultModel)?.Data as User)?.IsActive);

        }

        [Fact]
        public async Task CreateUserErrorCnpjCpfEmpty()
        {
            // Arrange
            var notifications = new List<Notification> {
                new Notification("CnpjCpf", UserMessage.CnpjCpfInvalid)
            };

            var commandResult = new GenericCommandResult(false, UserMessage.Validate, notifications);
            _mockHandle.Handle(Arg.Any<CreateUserCommand>()).Returns(commandResult);
            var application = new UserHandlerApplication(_mapperConfig, _mockHandle);
            var command = new CreateUserCommandApplication("Fake", "Teste123", string.Empty);

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.False((result as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.Validate, (result as CommandResultModel)?.Message);
            Assert.Equal(UserMessage.CnpjCpfInvalid, ((result as CommandResultModel)?.Data as List<Notification>)?[0].Message);
            Assert.Equal("CnpjCpf", ((result as CommandResultModel)?.Data as List<Notification>)?[0].Property);
        }

        [Fact]
        public async Task CreateUserErrorPasswordEmpty()
        {
            // Arrange
            var notifications = new List<Notification> {
                new Notification("Password", UserMessage.PasswordMinCharacter)
            };

            var commandResult = new GenericCommandResult(false, UserMessage.Validate, notifications);
            _mockHandle.Handle(Arg.Any<CreateUserCommand>()).Returns(commandResult);
            var application = new UserHandlerApplication(_mapperConfig, _mockHandle);
            var command = new CreateUserCommandApplication("Fake", "Teste123", string.Empty);

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.False((result as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.Validate, (result as CommandResultModel)?.Message);
            Assert.Equal(UserMessage.PasswordMinCharacter, ((result as CommandResultModel)?.Data as List<Notification>)?[0].Message);
            Assert.Equal("Password", ((result as CommandResultModel)?.Data as List<Notification>)?[0].Property);
        }

        [Fact]
        public async Task CreateUserErrorNameAndCnpjCpfInvalid()
        {
            // Arrange
            var notifications = new List<Notification> {
                new Notification("Name", UserMessage.UserNameNotInforming),
                new Notification("CnpjCpf", UserMessage.CnpjCpfInvalid)
            };

            var commandResult = new GenericCommandResult(false, UserMessage.Validate, notifications);
            _mockHandle.Handle(Arg.Any<CreateUserCommand>()).Returns(commandResult);
            var application = new UserHandlerApplication(_mapperConfig, _mockHandle);
            var command = new CreateUserCommandApplication(string.Empty, "Teste123", string.Empty);

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.False((result as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.Validate, (result as CommandResultModel)?.Message);
            Assert.Equal(UserMessage.UserNameNotInforming, ((result as CommandResultModel)?.Data as List<Notification>)?[0].Message);
            Assert.Equal("Name", ((result as CommandResultModel)?.Data as List<Notification>)?[0].Property);
            Assert.Equal(UserMessage.CnpjCpfInvalid, ((result as CommandResultModel)?.Data as List<Notification>)?[1].Message);
            Assert.Equal("CnpjCpf", ((result as CommandResultModel)?.Data as List<Notification>)?[1].Property);
        }

        [Fact]
        public async Task DeleteUserErrorIdInvalid()
        {
            // Arrange
            var notifications = new List<Notification> {
                new Notification("id", UserMessage.IdInvalid)
            };

            var commandResult = new GenericCommandResult(false, UserMessage.Validate, notifications);
            _mockHandle.Handle(Arg.Any<DeleteUserCommand>()).Returns(commandResult);
            var application = new UserHandlerApplication(_mapperConfig, _mockHandle);
            var command = new DeleteUserCommandApplication(Guid.Empty);

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.False((result as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.Validate, (result as CommandResultModel)?.Message);
            Assert.Equal(UserMessage.IdInvalid, ((result as CommandResultModel)?.Data as List<Notification>)?[0].Message);
            Assert.Equal("id", ((result as CommandResultModel)?.Data as List<Notification>)?[0].Property);
        }

        [Fact]
        public async Task GetByNameCnpjCpfUserErrorIdInvalid()
        {
            // Arrange
            var notifications = new List<Notification> {
                new Notification("", UserMessage.NameOrCnpjCpf)
            };

            var commandResult = new GenericCommandResult(false, UserMessage.Validate, notifications);
            _mockHandle.Handle(Arg.Any<GetByNameCnpjCpfUserCommand>()).Returns(commandResult);
            var application = new UserHandlerApplication(_mapperConfig, _mockHandle);
            var command = new GetByNameCnpjCpfUserCommandApplication(string.Empty, string.Empty);

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.False((result as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.Validate, (result as CommandResultModel)?.Message);
            Assert.Equal(UserMessage.NameOrCnpjCpf, ((result as CommandResultModel)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task GetByIdUserErrorIdInvalid()
        {
            // Arrange
            var notifications = new List<Notification> {
                new Notification("id", UserMessage.IdInvalid)
            };

            var commandResult = new GenericCommandResult(false, UserMessage.Validate, notifications);
            _mockHandle.Handle(Arg.Any<GetByIdUserCommand>()).Returns(commandResult);
            var application = new UserHandlerApplication(_mapperConfig, _mockHandle);
            var command = new GetByIdUserCommandApplication(Guid.Empty);

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.False((result as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.Validate, (result as CommandResultModel)?.Message);
            Assert.Equal(UserMessage.IdInvalid, ((result as CommandResultModel)?.Data as List<Notification>)?[0].Message);
            Assert.Equal("id", ((result as CommandResultModel)?.Data as List<Notification>)?[0].Property);
        }
    }
}