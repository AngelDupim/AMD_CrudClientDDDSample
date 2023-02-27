using AMD_CrudClientDDDSample.Application.Command;
using AMD_CrudClientDDDSample.Application.Command.User;
using AMD_CrudClientDDDSample.Application.Handler.Interfaces;
using AMD_CrudClientDDDSample.Domain.Entity;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Messages;
using AMD_CrudClientDDDSample.Services.Controllers;
using AMD_CrudClientDDDSample.Test.Repository;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AMD_CrudClientDDDSample.Test.Services
{
    public class UserServicesTest
    {
        private IUserHendlerApplication _mockHandle;
        private User _fakeUserEntity;
        private User _fakeUserPJEntity;
        private List<User> _listFakeUserEntity;

        public UserServicesTest()
        {
            _mockHandle = Substitute.For<IUserHendlerApplication>();
            _fakeUserEntity = FakeUserRepository.FakeUser(true);
            _fakeUserPJEntity = FakeUserRepository.FakeUser(false);
            _listFakeUserEntity = FakeUserRepository.ListFakeUser();
        }

        [Fact]
        public async Task CreateUserPFSucess()
        {
            // Arrange
            var commandResult = new CommandResultModel(true, UserMessage.CreateSucess, _fakeUserEntity);
            var command = new CreateUserCommandApplication("Fake", "Teste123", "13586155000");
            _mockHandle.Handle(Arg.Any<CreateUserCommandApplication>()).Returns(commandResult);
            var controller = new UserController(_mockHandle);

            // Act
            var result = await controller.Create(command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.CreateSucess, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(_fakeUserEntity.Name, (((result?.Value as CommandResultModel)?.Data as User)?.Name));
            Assert.Equal(_fakeUserEntity.CnpjCpf, (((result?.Value as CommandResultModel)?.Data as User)?.CnpjCpf));
            Assert.Equal(_fakeUserEntity.Password, (((result?.Value as CommandResultModel)?.Data as User)?.Password));
            Assert.True(((result?.Value as CommandResultModel)?.Data as User)?.IsActive);
        }

        [Fact]
        public async Task CreateUserPJSucess()
        {
            // Arrange
            var commandResult = new CommandResultModel(true, UserMessage.CreateSucess, _fakeUserPJEntity);
            var command = new CreateUserCommandApplication("FakePJ", "Teste123", "67700676000169");
            _mockHandle.Handle(Arg.Any<CreateUserCommandApplication>()).Returns(commandResult);
            var controller = new UserController(_mockHandle);

            // Act
            var result = await controller.Create(command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.CreateSucess, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(_fakeUserPJEntity.Name, (((result?.Value as CommandResultModel)?.Data as User)?.Name));
            Assert.Equal(_fakeUserPJEntity.CnpjCpf, (((result?.Value as CommandResultModel)?.Data as User)?.CnpjCpf));
            Assert.Equal(_fakeUserPJEntity.Password, (((result?.Value as CommandResultModel)?.Data as User)?.Password));
            Assert.True(((result?.Value as CommandResultModel)?.Data as User)?.IsActive);
        }

        [Fact]
        public async Task DeleteUserSucess()
        {
            // Arrange
            var commandResult = new CommandResultModel(true, UserMessage.DeleteSucess, _fakeUserEntity);
            var command = new DeleteUserCommandApplication(Guid.NewGuid());
            _mockHandle.Handle(Arg.Any<DeleteUserCommandApplication>()).Returns(commandResult);
            var controller = new UserController(_mockHandle);

            // Act
            var result = await controller.Delete(command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.DeleteSucess, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(_fakeUserEntity.Name, (((result?.Value as CommandResultModel)?.Data as User)?.Name));
            Assert.Equal(_fakeUserEntity.CnpjCpf, (((result?.Value as CommandResultModel)?.Data as User)?.CnpjCpf));
            Assert.Equal(_fakeUserEntity.Password, (((result?.Value as CommandResultModel)?.Data as User)?.Password));
        }

        [Fact]
        public async Task GetByNamePFSucess()
        {
            // Arrange
            var commandResult = new CommandResultModel(true, UserMessage.GetSucess,
                _listFakeUserEntity.Where(c => c.Name.Equals("Fake")).ToList());
            _mockHandle.Handle(Arg.Any<GetByNameCnpjCpfUserCommandApplication>()).Returns(commandResult);
            var controller = new UserController(_mockHandle);

            // Act
            var result = await controller.GetByUserNameCnpjCpf("Fake", string.Empty) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.GetSucess, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(_fakeUserEntity.Name, (((result?.Value as CommandResultModel)?.Data as List<User>)?[0].Name));
            Assert.Equal(_fakeUserEntity.CnpjCpf, (((result?.Value as CommandResultModel)?.Data as List<User>)?[0].CnpjCpf));
            Assert.Equal(_fakeUserEntity.Password, (((result?.Value as CommandResultModel)?.Data as List<User>)?[0].Password));
        }

        [Fact]
        public async Task GetByNamePJSucess()
        {
            // Arrange
            var commandResult = new CommandResultModel(true, UserMessage.GetSucess,
                _listFakeUserEntity.Where(c => c.Name.Equals("FakePJ")).ToList());
            _mockHandle.Handle(Arg.Any<GetByNameCnpjCpfUserCommandApplication>()).Returns(commandResult);
            var controller = new UserController(_mockHandle);

            // Act
            var result = await controller.GetByUserNameCnpjCpf("FakePJ", string.Empty) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.GetSucess, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(_fakeUserPJEntity.Name, (((result?.Value as CommandResultModel)?.Data as List<User>)?[0].Name));
            Assert.Equal(_fakeUserPJEntity.CnpjCpf, (((result?.Value as CommandResultModel)?.Data as List<User>)?[0].CnpjCpf));
            Assert.Equal(_fakeUserPJEntity.Password, (((result?.Value as CommandResultModel)?.Data as List<User>)?[0].Password));
        }

        [Fact]
        public async Task GetByCpfSucess()
        {
            // Arrange
            var commandResult = new CommandResultModel(true, UserMessage.GetSucess,
                _listFakeUserEntity.Where(c => c.CnpjCpf.Equals("13586155000")).ToList());
            _mockHandle.Handle(Arg.Any<GetByNameCnpjCpfUserCommandApplication>()).Returns(commandResult);
            var controller = new UserController(_mockHandle);

            // Act
            var result = await controller.GetByUserNameCnpjCpf(string.Empty, "13586155000") as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.GetSucess, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(_fakeUserEntity.Name, (((result?.Value as CommandResultModel)?.Data as List<User>)?[0].Name));
            Assert.Equal(_fakeUserEntity.CnpjCpf, (((result?.Value as CommandResultModel)?.Data as List<User>)?[0].CnpjCpf));
            Assert.Equal(_fakeUserEntity.Password, (((result?.Value as CommandResultModel)?.Data as List<User>)?[0].Password));
        }

        [Fact]
        public async Task GetByCnpjSucess()
        {
            // Arrange
            var commandResult = new CommandResultModel(true, UserMessage.GetSucess,
                _listFakeUserEntity.Where(c => c.CnpjCpf.Equals("67700676000169")).ToList());  
            _mockHandle.Handle(Arg.Any<GetByNameCnpjCpfUserCommandApplication>()).Returns(commandResult);
            var controller = new UserController(_mockHandle);

            // Act
            var result = await controller.GetByUserNameCnpjCpf(string.Empty, "67700676000169") as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.GetSucess, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(_fakeUserPJEntity.Name, (((result?.Value as CommandResultModel)?.Data as List<User>)?[0].Name));
            Assert.Equal(_fakeUserPJEntity.CnpjCpf, (((result?.Value as CommandResultModel)?.Data as List<User>)?[0].CnpjCpf));
            Assert.Equal(_fakeUserPJEntity.Password, (((result?.Value as CommandResultModel)?.Data as List<User>)?[0].Password));
        }

        [Fact]
        public async Task GetUserByIdSucess()
        {
            // Arrange
            var commandResult = new CommandResultModel(true, UserMessage.GetSucess, _fakeUserEntity);
            _mockHandle.Handle(Arg.Any<GetByIdUserCommandApplication>()).Returns(commandResult);
            var controller = new UserController(_mockHandle);

            // Act
            var result = await controller.GetByIdUser(Guid.NewGuid()) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.GetSucess, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(_fakeUserEntity.Name, (((result?.Value as CommandResultModel)?.Data as User)?.Name));
            Assert.Equal(_fakeUserEntity.CnpjCpf, (((result?.Value as CommandResultModel)?.Data as User)?.CnpjCpf));
            Assert.Equal(_fakeUserEntity.Password, (((result?.Value as CommandResultModel)?.Data as User)?.Password));
        }

        [Fact]
        public async Task CreateUserErrorCnpjCpfEmpty()
        {
            // Arrange
            var notifications = new List<Notification> {
                    new Notification("CnpjCpf", UserMessage.CnpjCpfInvalid)
            };

            var commandResult = new CommandResultModel(false, UserMessage.Validate, notifications);
            var command = new CreateUserCommandApplication("Fake", "Teste123", string.Empty);
            _mockHandle.Handle(Arg.Any<CreateUserCommandApplication>()).Returns(commandResult);
            var controller = new UserController(_mockHandle);

            // Act
            var result = await controller.Create(command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.False((result?.Value as CommandResultModel)?.Success);
            Assert.False((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.Validate, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(UserMessage.CnpjCpfInvalid, ((result?.Value as CommandResultModel)?.Data as List<Notification>)?[0].Message);
            Assert.Equal("CnpjCpf", ((result?.Value as CommandResultModel)?.Data as List<Notification>)?[0].Property);
        }

        [Fact]
        public async Task CreateUserErrorPasswordEmpty()
        {
            // Arrange
            var notifications = new List<Notification> {
                    new Notification("Password", UserMessage.PasswordMinCharacter)
            };

            var commandResult = new CommandResultModel(false, UserMessage.Validate, notifications);
            var command = new CreateUserCommandApplication("Fake", string.Empty, "13586155000");
            _mockHandle.Handle(Arg.Any<CreateUserCommandApplication>()).Returns(commandResult);
            var controller = new UserController(_mockHandle);

            // Act
            var result = await controller.Create(command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.False((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.Validate, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(UserMessage.PasswordMinCharacter, ((result?.Value as CommandResultModel)?.Data as List<Notification>)?[0].Message);
            Assert.Equal("Password", ((result?.Value as CommandResultModel)?.Data as List<Notification>)?[0].Property);
        }

        [Fact]
        public async Task CreateUserErrorNameAndCnpjCpfInvalid()
        {
            // Arrange
            var notifications = new List<Notification> {
                    new Notification("Name", UserMessage.UserNameNotInforming),
                    new Notification("CnpjCpf", UserMessage.CnpjCpfInvalid)
                };

            var commandResult = new CommandResultModel(false, UserMessage.Validate, notifications);
            var command = new CreateUserCommandApplication(string.Empty, "Teste123", string.Empty);
            _mockHandle.Handle(Arg.Any<CreateUserCommandApplication>()).Returns(commandResult);            
            var controller = new UserController(_mockHandle);

            // Act
            var result = await controller.Create(command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.False((result.Value as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.Validate, (result.Value as CommandResultModel)?.Message);
            Assert.Equal(UserMessage.UserNameNotInforming, ((result.Value as CommandResultModel)?.Data as List<Notification>)?[0].Message);
            Assert.Equal("Name", ((result.Value as CommandResultModel)?.Data as List<Notification>)?[0].Property);
            Assert.Equal(UserMessage.CnpjCpfInvalid, ((result.Value as CommandResultModel)?.Data as List<Notification>)?[1].Message);
            Assert.Equal("CnpjCpf", ((result.Value as CommandResultModel)?.Data as List<Notification>)?[1].Property);
        }

        [Fact]
        public async Task DeleteUserErrorIdInvalid()
        {
            // Arrange
            var notifications = new List<Notification> {
                    new Notification("id", UserMessage.IdInvalid)
            };

            var commandResult = new CommandResultModel(false, UserMessage.Validate, notifications);
            var commad = new DeleteUserCommandApplication(Guid.Empty);
            _mockHandle.Handle(Arg.Any<DeleteUserCommandApplication>()).Returns(commandResult);
            var controller = new UserController(_mockHandle);

            // Act
            var result = await controller.Delete(commad) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.False((result.Value as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.Validate, (result.Value as CommandResultModel)?.Message);
            Assert.Equal(UserMessage.IdInvalid, ((result.Value as CommandResultModel)?.Data as List<Notification>)?[0].Message);
            Assert.Equal("id", ((result.Value as CommandResultModel)?.Data as List<Notification>)?[0].Property);
        }

        [Fact]
        public async Task GetByNameCnpjCpfUserErrorIdInvalid()
        {
            // Arrange
            var notifications = new List<Notification> {
                    new Notification("", UserMessage.NameOrCnpjCpf)
            };

            var commandResult = new CommandResultModel(false, UserMessage.Validate, notifications);
            _mockHandle.Handle(Arg.Any<GetByNameCnpjCpfUserCommandApplication>()).Returns(commandResult);
            var controller = new UserController(_mockHandle);

            // Act
            var result = await controller.GetByUserNameCnpjCpf(string.Empty, string.Empty) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.False((result.Value as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.Validate, (result.Value as CommandResultModel)?.Message);
            Assert.Equal(UserMessage.NameOrCnpjCpf, ((result.Value as CommandResultModel)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task GetByIdUserErrorIdInvalid()
        {
            // Arrange
            var notifications = new List<Notification> {
                    new Notification("id", UserMessage.IdInvalid)
                };

            var commandResult = new CommandResultModel(false, UserMessage.Validate, notifications);
            _mockHandle.Handle(Arg.Any<GetByIdUserCommandApplication>()).Returns(commandResult);
            var controller = new UserController(_mockHandle);

            // Act
            var result = await controller.GetByIdUser(Guid.Empty) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.False((result.Value as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.Validate, (result.Value as CommandResultModel)?.Message);
            Assert.Equal(UserMessage.IdInvalid, ((result.Value as CommandResultModel)?.Data as List<Notification>)?[0].Message);
            Assert.Equal("id", ((result.Value as CommandResultModel)?.Data as List<Notification>)?[0].Property);
        }

        [Fact]
        public async Task CreateUserErroExceptionController()
        {
            // Arrange
            var command = new CreateUserCommandApplication(string.Empty, string.Empty, string.Empty);
            _mockHandle.Handle(Arg.Any<CreateUserCommandApplication>()).Throws(new Exception(UserMessage.CreateError));
            var controller = new UserController(_mockHandle);

            // Act
            var result = await controller.Create(command) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.False((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.CreateError, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(UserMessage.CreateError, (result?.Value as CommandResultModel)?.Data);
        }

        [Fact]
        public async Task DeleteUserErroExceptionController()
        {
            // Arrange
            var command = new DeleteUserCommandApplication(Guid.Empty);
            _mockHandle.Handle(Arg.Any<DeleteUserCommandApplication>()).Throws(new Exception(UserMessage.DeleteError));
            var controller = new UserController(_mockHandle);

            // Act
            var result = await controller.Delete(command) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.False((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.DeleteError, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(UserMessage.DeleteError, (result?.Value as CommandResultModel)?.Data);
        }

        [Fact]
        public async Task GetByNameCnpjCpfUserErroExceptionController()
        {
            // Arrange
            var command = new GetByNameCnpjCpfUserCommandApplication(string.Empty, string.Empty);
            _mockHandle.Handle(Arg.Any<GetByNameCnpjCpfUserCommandApplication>()).Throws(new Exception(UserMessage.GetError));
            var controller = new UserController(_mockHandle);

            // Act
            var result = await controller.GetByUserNameCnpjCpf(string.Empty, string.Empty) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.False((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.GetError, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(UserMessage.GetError, (result?.Value as CommandResultModel)?.Data);
        }

        [Fact]
        public async Task GetByIdUserErroExceptionController()
        {
            // Arrange
            _mockHandle.Handle(Arg.Any<GetByIdUserCommandApplication>()).Throws(new Exception(UserMessage.GetError));
            var controller = new UserController(_mockHandle);

            // Act
            var result = await controller.GetByIdUser(Guid.Empty) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.False((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(UserMessage.GetError, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(UserMessage.GetError, (result?.Value as CommandResultModel)?.Data);
        }
    }
}