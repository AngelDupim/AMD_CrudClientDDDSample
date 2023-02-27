using AMD_CrudClientDDDSample.Application.Command;
using AMD_CrudClientDDDSample.Application.Command.Client;
using AMD_CrudClientDDDSample.Application.Command.Interfaces;
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
    public class ClientServicesTest
    {
        private IClientHandlerApplication _mockHandler;
        private Client _fakeClientEntity;
        private Client _fakeClientPJEntity;
        private List<Client> _fakeListClients;

        public ClientServicesTest()
        {
            _mockHandler = Substitute.For<IClientHandlerApplication>();
            _fakeClientEntity = FakeClientRepository.FakeClient(true);
            _fakeListClients = FakeClientRepository.ListFakeClient();
            _fakeClientPJEntity = FakeClientRepository.FakeClient(false);
        }

        [Fact]
        public async Task CreateClientPFSucess()
        {
            // Arrange
            ICommandResultModel commandResult = new CommandResultModel(true, ClientMessage.CreateSucess, _fakeClientEntity);
            var command = new CreateClientCommandApplication("Fake", "13586155000", DateTime.Now);
            _mockHandler.Handle(Arg.Any<CreateClientCommandApplication>()).Returns(commandResult);
            var controller = new ClientController(_mockHandler);

            // Act
            var result = await controller.Create(command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.CreateSucess, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(_fakeClientEntity.Name, (((result?.Value as CommandResultModel)?.Data as Client)?.Name));
            Assert.Equal(_fakeClientEntity.CnpjCpf, (((result?.Value as CommandResultModel)?.Data as Client)?.CnpjCpf));
            Assert.Equal(_fakeClientEntity.BirthDate, (((result?.Value as CommandResultModel)?.Data as Client)?.BirthDate.Date));
            Assert.NotNull(((result?.Value as CommandResultModel)?.Data as Client)?.BirthDate.Date);
        }

        [Fact]
        public async Task CreateClientPJSucess()
        {
            // Arrange
            ICommandResultModel commandResult = new CommandResultModel(true, ClientMessage.CreateSucess, _fakeClientPJEntity);
            var command = new CreateClientCommandApplication("FakePJ", "67700676000169", DateTime.Now);
            _mockHandler.Handle(Arg.Any<CreateClientCommandApplication>()).Returns(commandResult);
            var controller = new ClientController(_mockHandler);

            // Act
            var result = await controller.Create(command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.CreateSucess, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(_fakeClientPJEntity.Name, (((result?.Value as CommandResultModel)?.Data as Client)?.Name));
            Assert.Equal(_fakeClientPJEntity.CnpjCpf, (((result?.Value as CommandResultModel)?.Data as Client)?.CnpjCpf));
            Assert.Equal(_fakeClientPJEntity.BirthDate, (((result?.Value as CommandResultModel)?.Data as Client)?.BirthDate.Date));
            Assert.NotNull(((result?.Value as CommandResultModel)?.Data as Client)?.BirthDate.Date);
        }

        [Fact]
        public async Task UpdateClientSucess()
        {
            // Arrange
            ICommandResultModel commandResult = new CommandResultModel(true, ClientMessage.UpdateSucess, _fakeClientEntity);
            var command = new UpdateClientCommandApplication(Guid.NewGuid(), "Fake");
            _mockHandler.Handle(Arg.Any<UpdateClientCommandApplication>()).Returns(commandResult);
            var controller = new ClientController(_mockHandler);

            // Act
            var result = await controller.Update(command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.UpdateSucess, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(_fakeClientEntity.Name, (((result?.Value as CommandResultModel)?.Data as Client)?.Name));
            Assert.Equal(_fakeClientEntity.CnpjCpf, (((result?.Value as CommandResultModel)?.Data as Client)?.CnpjCpf));
            Assert.Equal(_fakeClientEntity.BirthDate, (((result?.Value as CommandResultModel)?.Data as Client)?.BirthDate.Date));
            Assert.NotNull(((result?.Value as CommandResultModel)?.Data as Client)?.BirthDate.Date);
        }

        [Fact]
        public async Task GetByCpfClientSucess()
        {
            // Arrange
            ICommandResultModel commandResult = new CommandResultModel(true, ClientMessage.GetSucess,
                _fakeListClients.Where(c => c.CnpjCpf.Equals("13586155000")).ToList());
            _mockHandler.Handle(Arg.Any<GetByNameCnpjCpfClientCommandApplication>()).Returns(commandResult);
            var controller = new ClientController(_mockHandler);

            // Act
            var result = await controller.GetByNameOrCnpjCpf(string.Empty, "13586155000") as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.GetSucess, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(_fakeClientEntity.Name, (((result?.Value as CommandResultModel)?.Data as List<Client>)?[0].Name));
            Assert.Equal(_fakeClientEntity.CnpjCpf, (((result?.Value as CommandResultModel)?.Data as List<Client>)?[0].CnpjCpf));
            Assert.Equal(_fakeClientEntity.BirthDate, (((result?.Value as CommandResultModel)?.Data as List<Client>)?[0].BirthDate.Date));
            Assert.NotNull(((result?.Value as CommandResultModel)?.Data as List<Client>)?[0].BirthDate.Date);
        }

        [Fact]
        public async Task GetByCnpjClientSucess()
        {
            // Arrange
            ICommandResultModel commandResult = new CommandResultModel(true, ClientMessage.GetSucess,
                _fakeListClients.Where(c => c.CnpjCpf.Equals("67700676000169")).ToList());
            _mockHandler.Handle(Arg.Any<GetByNameCnpjCpfClientCommandApplication>()).Returns(commandResult);
            var controller = new ClientController(_mockHandler);

            // Act
            var result = await controller.GetByNameOrCnpjCpf(string.Empty, "67700676000169") as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.GetSucess, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(_fakeClientPJEntity.Name, (((result?.Value as CommandResultModel)?.Data as List<Client>)?[0].Name));
            Assert.Equal(_fakeClientPJEntity.CnpjCpf, (((result?.Value as CommandResultModel)?.Data as List<Client>)?[0].CnpjCpf));
            Assert.Equal(_fakeClientPJEntity.BirthDate, (((result?.Value as CommandResultModel)?.Data as List<Client>)?[0].BirthDate.Date));
            Assert.NotNull(((result?.Value as CommandResultModel)?.Data as List<Client>)?[0].BirthDate.Date);
        }

        [Fact]
        public async Task GetByNamePFClientSucess()
        {
            // Arrange
            ICommandResultModel commandResult = new CommandResultModel(true, ClientMessage.GetSucess,
                _fakeListClients.Where(c => c.Name.Equals("Fake")).ToList());
            _mockHandler.Handle(Arg.Any<GetByNameCnpjCpfClientCommandApplication>()).Returns(commandResult);
            var controller = new ClientController(_mockHandler);

            // Act
            var result = await controller.GetByNameOrCnpjCpf("Fake", string.Empty) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.GetSucess, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(_fakeClientEntity.Name, (((result?.Value as CommandResultModel)?.Data as List<Client>)?[0].Name));
            Assert.Equal(_fakeClientEntity.CnpjCpf, (((result?.Value as CommandResultModel)?.Data as List<Client>)?[0].CnpjCpf));
            Assert.Equal(_fakeClientEntity.BirthDate, (((result?.Value as CommandResultModel)?.Data as List<Client>)?[0].BirthDate.Date));
            Assert.NotNull(((result?.Value as CommandResultModel)?.Data as List<Client>)?[0].BirthDate.Date);
        }

        [Fact]
        public async Task GetByNamePJClientSucess()
        {
            // Arrange
            ICommandResultModel commandResult = new CommandResultModel(true, ClientMessage.GetSucess,
                _fakeListClients.Where(c => c.Name.Equals("FakePJ")).ToList());
            _mockHandler.Handle(Arg.Any<GetByNameCnpjCpfClientCommandApplication>()).Returns(commandResult);
            var controller = new ClientController(_mockHandler);

            // Act
            var result = await controller.GetByNameOrCnpjCpf("FakePJ", string.Empty) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.GetSucess, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(_fakeClientPJEntity.Name, (((result?.Value as CommandResultModel)?.Data as List<Client>)?[0].Name));
            Assert.Equal(_fakeClientPJEntity.CnpjCpf, (((result?.Value as CommandResultModel)?.Data as List<Client>)?[0].CnpjCpf));
            Assert.Equal(_fakeClientPJEntity.BirthDate, (((result?.Value as CommandResultModel)?.Data as List<Client>)?[0].BirthDate.Date));
            Assert.NotNull(((result?.Value as CommandResultModel)?.Data as List<Client>)?[0].BirthDate.Date);
        }

        [Fact]
        public async Task GetByIdClientSucess()
        {
            // Arrange
            ICommandResultModel commandResult = new CommandResultModel(true, ClientMessage.GetSucess, _fakeClientEntity);
            _mockHandler.Handle(Arg.Any<GetByIdClientCommandApplication>()).Returns(commandResult);
            var controller = new ClientController(_mockHandler);

            // Act
            var result = await controller.GetById(Guid.NewGuid()) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.True((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.GetSucess, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(_fakeClientEntity.Name, (((result?.Value as CommandResultModel)?.Data as Client)?.Name));
            Assert.Equal(_fakeClientEntity.CnpjCpf, (((result?.Value as CommandResultModel)?.Data as Client)?.CnpjCpf));
            Assert.Equal(_fakeClientEntity.BirthDate, (((result?.Value as CommandResultModel)?.Data as Client)?.BirthDate.Date));
            Assert.NotNull(((result?.Value as CommandResultModel)?.Data as Client)?.BirthDate.Date);
        }

        [Fact]
        public async Task CreateClientNameCnpjCpfEmpty()
        {
            // Arrange
            var notifications = new List<Notification> {
                new Notification("Name", ClientMessage.NameNotInforming),
                new Notification("CnpjCpf", ClientMessage.CnpjCpfInvalid)
            };

            ICommandResultModel commandResult = new CommandResultModel(false, ClientMessage.Validate, notifications);
            var command = new CreateClientCommandApplication(string.Empty, string.Empty, DateTime.Now);
            _mockHandler.Handle(Arg.Any<CreateClientCommandApplication>()).Returns(commandResult);
            var controller = new ClientController(_mockHandler);

            // Act
            var result = await controller.Create(command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.False((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.Validate, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(ClientMessage.NameNotInforming, ((result?.Value as CommandResultModel)?.Data as List<Notification>)?[0].Message);
            Assert.Equal("Name", ((result?.Value as CommandResultModel)?.Data as List<Notification>)?[0].Property);
            Assert.Equal(ClientMessage.CnpjCpfInvalid, ((result?.Value as CommandResultModel)?.Data as List<Notification>)?[1].Message);
            Assert.Equal("CnpjCpf", ((result?.Value as CommandResultModel)?.Data as List<Notification>)?[1].Property);
        }

        [Fact]
        public async Task UpdateClientNameAndIdInvalid()
        {
            // Arrange
            var notifications = new List<Notification> {
                new Notification("Name", ClientMessage.NameNotInforming),
                new Notification("id", ClientMessage.IdInvalid)
            };

            ICommandResultModel commandResult = new CommandResultModel(false, ClientMessage.Validate, notifications);
            var command = new UpdateClientCommandApplication(Guid.Empty, string.Empty);
            _mockHandler.Handle(Arg.Any<UpdateClientCommandApplication>()).Returns(commandResult);
            var controller = new ClientController(_mockHandler);

            // Act
            var result = await controller.Update(command) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.False((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.Validate, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(ClientMessage.NameNotInforming, ((result?.Value as CommandResultModel)?.Data as List<Notification>)?[0].Message);
            Assert.Equal("Name", ((result?.Value as CommandResultModel)?.Data as List<Notification>)?[0].Property);
            Assert.Equal(ClientMessage.IdInvalid, ((result?.Value as CommandResultModel)?.Data as List<Notification>)?[1].Message);
            Assert.Equal("id", ((result?.Value as CommandResultModel)?.Data as List<Notification>)?[1].Property);
        }

        [Fact]
        public async Task GetByNameCnpjCpfClientErrorIdInvalid()
        {
            // Arrange
            var notifications = new List<Notification> {
                new Notification("", ClientMessage.NameOrCnpjCpf)
            };

            ICommandResultModel commandResult = new CommandResultModel(false, ClientMessage.Validate, notifications);            
            _mockHandler.Handle(Arg.Any<GetByNameCnpjCpfClientCommandApplication>()).Returns(commandResult);
            var controller = new ClientController(_mockHandler);

            // Act
            var result = await controller.GetByNameOrCnpjCpf(string.Empty, string.Empty) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.False((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.Validate, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(ClientMessage.NameOrCnpjCpf, ((result?.Value as CommandResultModel)?.Data as List<Notification>)?[0].Message);

        }

        [Fact]
        public async Task GetByIdClientErrorIdInvalid()
        {
            // Arrange
            var notifications = new List<Notification> {
                new Notification("id", ClientMessage.IdInvalid)
            };

            ICommandResultModel commandResult = new CommandResultModel(false, ClientMessage.Validate, notifications);
            _mockHandler.Handle(Arg.Any<GetByIdClientCommandApplication>()).Returns(commandResult);
            var controller = new ClientController(_mockHandler);

            // Act
            var result = await controller.GetById(Guid.NewGuid()) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.False((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.Validate, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(ClientMessage.IdInvalid, ((result?.Value as CommandResultModel)?.Data as List<Notification>)?[0].Message);
            Assert.Equal("id", ((result?.Value as CommandResultModel)?.Data as List<Notification>)?[0].Property);
        }

        [Fact]
        public async Task CreateClientErroExceptionController()
        {
            // Arrange
            var command = new CreateClientCommandApplication(string.Empty, string.Empty, DateTime.Now);
            _mockHandler.Handle(Arg.Any<CreateClientCommandApplication>()).Throws(new Exception(ClientMessage.CreateError));
            var controller = new ClientController(_mockHandler);

            // Act
            var result = await controller.Create(command) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.False((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.CreateError, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(ClientMessage.CreateError, (result?.Value as CommandResultModel)?.Data);
        }

        [Fact]
        public async Task UpdateClientErroExceptionController()
        {
            // Arrange
            var command = new UpdateClientCommandApplication(Guid.Empty, string.Empty);
            _mockHandler.Handle(Arg.Any<UpdateClientCommandApplication>()).Throws(new Exception(ClientMessage.UpdateError));
            var controller = new ClientController(_mockHandler);

            // Act
            var result = await controller.Update(command) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.False((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.UpdateError, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(ClientMessage.UpdateError, (result?.Value as CommandResultModel)?.Data);
        }

        [Fact]
        public async Task GetByNameCnpjCpfClientErroExceptionController()
        {
            // Arrange
            _mockHandler.Handle(Arg.Any<GetByNameCnpjCpfClientCommandApplication>()).Throws(new Exception(ClientMessage.GetError));
            var controller = new ClientController(_mockHandler);

            // Act
            var result = await controller.GetByNameOrCnpjCpf(string.Empty, string.Empty) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.False((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.GetError, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(ClientMessage.GetError, (result?.Value as CommandResultModel)?.Data);
        }

        [Fact]
        public async Task GetByIdClientErroExceptionController()
        {
            // Arrange
            _mockHandler.Handle(Arg.Any<GetByIdClientCommandApplication>()).Throws(new Exception(ClientMessage.GetError));
            var controller = new ClientController(_mockHandler);

            // Act
            var result = await controller.GetById(Guid.Empty) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.False((result?.Value as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.GetError, (result?.Value as CommandResultModel)?.Message);
            Assert.Equal(ClientMessage.GetError, (result?.Value as CommandResultModel)?.Data);
        }
    }
}