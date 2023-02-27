using AMD_CrudClientDDDSample.Application.Command;
using AMD_CrudClientDDDSample.Application.Command.Client;
using AMD_CrudClientDDDSample.Application.Handler;
using AMD_CrudClientDDDSample.Application.Mapper;
using AMD_CrudClientDDDSample.Domain.Command;
using AMD_CrudClientDDDSample.Domain.Command.Client;
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
    public class ClientApplicationTest
    {
        private Client _fakeClientEntity;
        private Client _fakeClientPJEntity;
        private List<Client> _listFakeClientEntity;
        private IClientHandler _mockHandle;
        private IMapper _mapperConfig;

        public ClientApplicationTest()
        {
            _mockHandle = Substitute.For<IClientHandler>();
            _fakeClientEntity = FakeClientRepository.FakeClient(true);
            _fakeClientPJEntity = FakeClientRepository.FakeClient(false);
            _listFakeClientEntity = FakeClientRepository.ListFakeClient();
            _mapperConfig = MapperConfig.RegisterMapper();
        }

        [Fact]
        public async Task CreateClientPFSucess()
        {
            // Arrange
            var commandResult = new GenericCommandResult(true, ClientMessage.CreateSucess, _fakeClientEntity);
            _mockHandle.Handle(Arg.Any<CreateClientCommand>()).Returns(commandResult);
            var application = new ClientHandlerApplication(_mapperConfig, _mockHandle);
            var command = new CreateClientCommandApplication("Fake", "13586155000", DateTime.Now);

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.CreateSucess, (result as CommandResultModel)?.Message);
            Assert.Equal(_fakeClientEntity.Name, ((result as CommandResultModel)?.Data as Client)?.Name);
            Assert.Equal(_fakeClientEntity.CnpjCpf, ((result as CommandResultModel)?.Data as Client)?.CnpjCpf);
            Assert.Equal(_fakeClientEntity.BirthDate.Date, ((result as CommandResultModel)?.Data as Client)?.BirthDate.Date);
            Assert.NotNull(((result as CommandResultModel)?.Data as Client)?.RegisterDate);
        }

        [Fact]
        public async Task CreateClientPJSucess()
        {
            // Arrange
            var commandResult = new GenericCommandResult(true, ClientMessage.CreateSucess, _fakeClientPJEntity);
            _mockHandle.Handle(Arg.Any<CreateClientCommand>()).Returns(commandResult);
            var application = new ClientHandlerApplication(_mapperConfig, _mockHandle);
            var command = new CreateClientCommandApplication("FakePJ", "67700676000169", DateTime.Now);

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.CreateSucess, (result as CommandResultModel)?.Message);
            Assert.Equal(_fakeClientPJEntity.Name, ((result as CommandResultModel)?.Data as Client)?.Name);
            Assert.Equal(_fakeClientPJEntity.CnpjCpf, ((result as CommandResultModel)?.Data as Client)?.CnpjCpf);
            Assert.Equal(_fakeClientPJEntity.BirthDate.Date, ((result as CommandResultModel)?.Data as Client)?.BirthDate.Date);
            Assert.NotNull(((result as CommandResultModel)?.Data as Client)?.RegisterDate);
        }

        [Fact]
        public async Task UpdateClientSucess()
        {
            // Arrange
            var commandResult = new GenericCommandResult(true, ClientMessage.UpdateSucess, _fakeClientEntity);
            _mockHandle.Handle(Arg.Any<UpdateClientCommand>()).Returns(commandResult);
            var application = new ClientHandlerApplication(_mapperConfig, _mockHandle);
            var command = new UpdateClientCommandApplication(Guid.NewGuid(), "Fake");

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.UpdateSucess, (result as CommandResultModel)?.Message);
            Assert.Equal(_fakeClientEntity.Name, ((result as CommandResultModel)?.Data as Client)?.Name);
            Assert.Equal(_fakeClientEntity.CnpjCpf, ((result as CommandResultModel)?.Data as Client)?.CnpjCpf);
            Assert.Equal(_fakeClientEntity.BirthDate.Date, ((result as CommandResultModel)?.Data as Client)?.BirthDate.Date);
            Assert.NotNull(((result as CommandResultModel)?.Data as Client)?.RegisterDate);
        }

        [Fact]
        public async Task GetByNamePFSucess()
        {
            // Arrange
            var commandResult = new GenericCommandResult(true, ClientMessage.GetSucess, 
                _listFakeClientEntity.Where(c => c.Name.Equals("Fake")).ToList());
            _mockHandle.Handle(Arg.Any<GetByNameCnpjCpfClientCommand>()).Returns(commandResult);
            var application = new ClientHandlerApplication(_mapperConfig, _mockHandle);
            var command = new GetByNameCnpjCpfClientCommandApplication("Fake", string.Empty);

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.GetSucess, (result as CommandResultModel)?.Message);
            Assert.Equal(_fakeClientEntity.Name, ((result as CommandResultModel)?.Data as List<Client>)?[0].Name);
            Assert.Equal(_fakeClientEntity.CnpjCpf, ((result as CommandResultModel)?.Data as List<Client>)?[0].CnpjCpf);
            Assert.Equal(_fakeClientEntity.BirthDate.Date, ((result as CommandResultModel)?.Data as List<Client>)?[0].BirthDate.Date);
            Assert.NotNull(((result as CommandResultModel)?.Data as List<Client>)?[0].RegisterDate);
        }

        [Fact]
        public async Task GetByNamePJSucess()
        {
            // Arrange
            var commandResult = new GenericCommandResult(true, ClientMessage.GetSucess,
                _listFakeClientEntity.Where(c => c.Name.Equals("FakePJ")).ToList());
            _mockHandle.Handle(Arg.Any<GetByNameCnpjCpfClientCommand>()).Returns(commandResult);
            var application = new ClientHandlerApplication(_mapperConfig, _mockHandle);
            var command = new GetByNameCnpjCpfClientCommandApplication("FakePJ", string.Empty);

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.GetSucess, (result as CommandResultModel)?.Message);
            Assert.Equal(_fakeClientPJEntity.Name, ((result as CommandResultModel)?.Data as List<Client>)?[0].Name);
            Assert.Equal(_fakeClientPJEntity.CnpjCpf, ((result as CommandResultModel)?.Data as List<Client>)?[0].CnpjCpf);
            Assert.Equal(_fakeClientPJEntity.BirthDate.Date, ((result as CommandResultModel)?.Data as List<Client>)?[0].BirthDate.Date);
            Assert.NotNull(((result as CommandResultModel)?.Data as List<Client>)?[0].RegisterDate);
        }

        [Fact]
        public async Task GetByCpfSucess()
        {
            // Arrange
            var commandResult = new GenericCommandResult(true, ClientMessage.GetSucess,
                _listFakeClientEntity.Where(c => c.CnpjCpf.Equals("13586155000")).ToList());
            _mockHandle.Handle(Arg.Any<GetByNameCnpjCpfClientCommand>()).Returns(commandResult);
            var application = new ClientHandlerApplication(_mapperConfig, _mockHandle);
            var command = new GetByNameCnpjCpfClientCommandApplication(string.Empty, "13586155000");

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.GetSucess, (result as CommandResultModel)?.Message);
            Assert.Equal(_fakeClientEntity.Name, ((result as CommandResultModel)?.Data as List<Client>)?[0].Name);
            Assert.Equal(_fakeClientEntity.CnpjCpf, ((result as CommandResultModel)?.Data as List<Client>)?[0].CnpjCpf);
            Assert.Equal(_fakeClientEntity.BirthDate.Date, ((result as CommandResultModel)?.Data as List<Client>)?[0].BirthDate.Date);
            Assert.NotNull(((result as CommandResultModel)?.Data as List<Client>)?[0].RegisterDate);
        }

        [Fact]
        public async Task GetByCnpjSucess()
        {
            // Arrange
            var commandResult = new GenericCommandResult(true, ClientMessage.GetSucess,
                _listFakeClientEntity.Where(c => c.CnpjCpf.Equals("67700676000169")).ToList());
            _mockHandle.Handle(Arg.Any<GetByNameCnpjCpfClientCommand>()).Returns(commandResult);
            var application = new ClientHandlerApplication(_mapperConfig, _mockHandle);
            var command = new GetByNameCnpjCpfClientCommandApplication(string.Empty, "67700676000169");

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.GetSucess, (result as CommandResultModel)?.Message);
            Assert.Equal(_fakeClientPJEntity.Name, ((result as CommandResultModel)?.Data as List<Client>)?[0].Name);
            Assert.Equal(_fakeClientPJEntity.CnpjCpf, ((result as CommandResultModel)?.Data as List<Client>)?[0].CnpjCpf);
            Assert.Equal(_fakeClientPJEntity.BirthDate.Date, ((result as CommandResultModel)?.Data as List<Client>)?[0].BirthDate.Date);
            Assert.NotNull(((result as CommandResultModel)?.Data as List<Client>)?[0].RegisterDate);
        }

        [Fact]
        public async Task GetClientByIdSucess()
        {
            // Arrange
            var commandResult = new GenericCommandResult(true, ClientMessage.GetSucess, _fakeClientEntity);
            _mockHandle.Handle(Arg.Any<GetByIdClientCommand>()).Returns(commandResult);
            var application = new ClientHandlerApplication(_mapperConfig, _mockHandle);
            var command = new GetByIdClientCommandApplication(Guid.NewGuid());

            // Act
            var result = await application.Handle(command);

            Assert.NotNull(result);
            Assert.True((result as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.GetSucess, (result as CommandResultModel)?.Message);
            Assert.Equal(_fakeClientEntity.Name, ((result as CommandResultModel)?.Data as Client)?.Name);
            Assert.Equal(_fakeClientEntity.CnpjCpf, ((result as CommandResultModel)?.Data as Client)?.CnpjCpf);
            Assert.Equal(_fakeClientEntity.BirthDate.Date, ((result as CommandResultModel)?.Data as Client)?.BirthDate.Date);
            Assert.NotNull(((result as CommandResultModel)?.Data as Client)?.RegisterDate);
        }

        [Fact]
        public async Task CreateClientErrorCnpjCpfEmpty()
        {
            // Arrange
            var notifications = new List<Notification> {
                new Notification("CnpjCpf", ClientMessage.CnpjCpfInvalid)
            };

            var commandResult = new GenericCommandResult(false, ClientMessage.Validate, notifications);
            _mockHandle.Handle(Arg.Any<CreateClientCommand>()).Returns(commandResult);
            var application = new ClientHandlerApplication(_mapperConfig, _mockHandle);
            var command = new CreateClientCommandApplication("Fake", string.Empty, DateTime.Now);

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.False((result as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.Validate, (result as CommandResultModel)?.Message);
            Assert.Equal(ClientMessage.CnpjCpfInvalid, ((result as CommandResultModel)?.Data as List<Notification>)?[0].Message);
            Assert.Equal("CnpjCpf", ((result as CommandResultModel)?.Data as List<Notification>)?[0].Property);
        }

        [Fact]
        public async Task CreateClientErrorNameAndCnpjCpfInvalid()
        {
            // Arrange
            var notifications = new List<Notification> {
                new Notification("Name", ClientMessage.NameNotInforming),
                new Notification("CnpjCpf", ClientMessage.CnpjCpfInvalid)
            };

            var commandResult = new GenericCommandResult(false, ClientMessage.Validate, notifications);
            _mockHandle.Handle(Arg.Any<CreateClientCommand>()).Returns(commandResult);
            var application = new ClientHandlerApplication(_mapperConfig, _mockHandle);
            var command = new CreateClientCommandApplication("Fake", string.Empty, DateTime.Now);

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.False((result as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.Validate, (result as CommandResultModel)?.Message);
            Assert.Equal(ClientMessage.NameNotInforming, ((result as CommandResultModel)?.Data as List<Notification>)?[0].Message);
            Assert.Equal("Name", ((result as CommandResultModel)?.Data as List<Notification>)?[0].Property);
            Assert.Equal(ClientMessage.CnpjCpfInvalid, ((result as CommandResultModel)?.Data as List<Notification>)?[1].Message);
            Assert.Equal("CnpjCpf", ((result as CommandResultModel)?.Data as List<Notification>)?[1].Property);
        }

        [Fact]
        public async Task UpdateClientErrorNameAndIdInvalid()
        {
            // Arrange
            var notifications = new List<Notification> {
                new Notification("Name", ClientMessage.NameNotInforming),
                new Notification("id", ClientMessage.IdInvalid)
            };

            var commandResult = new GenericCommandResult(false, ClientMessage.Validate, notifications);
            _mockHandle.Handle(Arg.Any<UpdateClientCommand>()).Returns(commandResult);
            var application = new ClientHandlerApplication(_mapperConfig, _mockHandle);
            var command = new UpdateClientCommandApplication(Guid.Empty, string.Empty);

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.False((result as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.Validate, (result as CommandResultModel)?.Message);
            Assert.Equal(ClientMessage.NameNotInforming, ((result as CommandResultModel)?.Data as List<Notification>)?[0].Message);
            Assert.Equal("Name", ((result as CommandResultModel)?.Data as List<Notification>)?[0].Property);
            Assert.Equal(ClientMessage.IdInvalid, ((result as CommandResultModel)?.Data as List<Notification>)?[1].Message);
            Assert.Equal("id", ((result as CommandResultModel)?.Data as List<Notification>)?[1].Property);
        }

        [Fact]
        public async Task GetByNameCnpjCpfClientErrorIdInvalid()
        {
            // Arrange
            var notifications = new List<Notification> {
                new Notification("", ClientMessage.NameOrCnpjCpf)
            };

            var commandResult = new GenericCommandResult(false, ClientMessage.Validate, notifications);
            _mockHandle.Handle(Arg.Any<GetByNameCnpjCpfClientCommand>()).Returns(commandResult);
            var application = new ClientHandlerApplication(_mapperConfig, _mockHandle);
            var command = new GetByNameCnpjCpfClientCommandApplication(string.Empty,string.Empty);

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.False((result as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.Validate, (result as CommandResultModel)?.Message);
            Assert.Equal(ClientMessage.NameOrCnpjCpf, ((result as CommandResultModel)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task GetByIdClientErrorIdInvalid()
        {
            // Arrange
            var notifications = new List<Notification> {
                new Notification("id", ClientMessage.IdInvalid)
            };

            var commandResult = new GenericCommandResult(false, ClientMessage.Validate, notifications);
            _mockHandle.Handle(Arg.Any<GetByIdClientCommand>()).Returns(commandResult);
            var application = new ClientHandlerApplication(_mapperConfig, _mockHandle);
            var command = new GetByIdClientCommandApplication(Guid.Empty);

            // Act
            var result = await application.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.False((result as CommandResultModel)?.Success);
            Assert.Equal(ClientMessage.Validate, (result as CommandResultModel)?.Message);
            Assert.Equal(ClientMessage.IdInvalid, ((result as CommandResultModel)?.Data as List<Notification>)?[0].Message);
            Assert.Equal("id", ((result as CommandResultModel)?.Data as List<Notification>)?[0].Property);
        }
    }
}