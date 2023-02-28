using AMD_CrudClientDDDSample.Domain.Command;
using AMD_CrudClientDDDSample.Domain.Command.Client;
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
    public class ClientDomainTest
    {
        private IClientRepository _mockClientRepository;
        private Client _fakeClientEntity;
        private Client _fakeClientPJEntity;
        private List<Client> _listFakeClientEntity;

        public ClientDomainTest()
        {
            _mockClientRepository = Substitute.For<IClientRepository>();
            _fakeClientEntity = FakeClientRepository.FakeClient(true);
            _fakeClientPJEntity = FakeClientRepository.FakeClient(false);
            _listFakeClientEntity = FakeClientRepository.ListFakeClient();
        }

        [Fact]
        public async Task CreateClientPFSucess()
        {
            // Arrange
            _mockClientRepository.Create(Arg.Any<Client>()).Returns(_fakeClientEntity);
            var command = new CreateClientCommand("Fake", "13586155000", DateTime.Now.Date);
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as GenericCommandResult)?.Success);
            Assert.Equal(_fakeClientEntity.Name, ((result as GenericCommandResult)?.Data as Client)?.Name);
            Assert.Equal(_fakeClientEntity.CnpjCpf, ((result as GenericCommandResult)?.Data as Client)?.CnpjCpf);
            Assert.Equal(_fakeClientEntity.BirthDate, ((result as GenericCommandResult)?.Data as Client)?.BirthDate);
            Assert.NotNull(((result as GenericCommandResult)?.Data as Client)?.RegisterDate);
        }

        [Fact]
        public async Task CreateClientPJSucess()
        {
            // Arrange
            _mockClientRepository.Create(Arg.Any<Client>()).Returns(_fakeClientPJEntity);
            var command = new CreateClientCommand("FakePJ", "67700676000169", DateTime.Now.Date);
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as GenericCommandResult)?.Success);
            Assert.Equal(_fakeClientPJEntity.Name, ((result as GenericCommandResult)?.Data as Client)?.Name);
            Assert.Equal(_fakeClientPJEntity.CnpjCpf, ((result as GenericCommandResult)?.Data as Client)?.CnpjCpf);
            Assert.Equal(_fakeClientPJEntity.BirthDate, ((result as GenericCommandResult)?.Data as Client)?.BirthDate);
            Assert.NotNull(((result as GenericCommandResult)?.Data as Client)?.RegisterDate);
        }

        [Fact]
        public async Task UpdateClientSucess()
        {
            // Arrange
            _mockClientRepository.GetById(Arg.Any<Guid>()).Returns(_fakeClientEntity);
            var command = new UpdateClientCommand(Guid.NewGuid(), "Fake2");
            var handler = new ClientHandler(_mockClientRepository);

            // Action
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as GenericCommandResult)?.Success);
            Assert.Equal("Fake2", ((result as GenericCommandResult)?.Data as Client)?.Name);
            Assert.Equal(_fakeClientEntity.CnpjCpf, ((result as GenericCommandResult)?.Data as Client)?.CnpjCpf);
            Assert.Equal(_fakeClientEntity.BirthDate, ((result as GenericCommandResult)?.Data as Client)?.BirthDate);
            Assert.NotNull(((result as GenericCommandResult)?.Data as Client)?.RegisterDate);
        }

        [Fact]
        public async Task GetClientByNameSucess()
        {
            // Arrange
            _mockClientRepository.GetByField(Arg.Any<Dictionary<object, object>>()).Returns(
                _listFakeClientEntity.Where(c => c.Name.Equals("Fake")).ToList());
            var command = new GetByNameCnpjCpfClientCommand("Fake", string.Empty);
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert            
            Assert.NotNull(result);
            Assert.True((result as GenericCommandResult)?.Success);
            Assert.Equal(_fakeClientEntity.Name, ((result as GenericCommandResult)?.Data as List<Client>)?[0].Name);
            Assert.Equal(_fakeClientEntity.CnpjCpf, ((result as GenericCommandResult)?.Data as List<Client>)?[0].CnpjCpf);
            Assert.Equal(_fakeClientEntity.BirthDate, ((result as GenericCommandResult)?.Data as List<Client>)?[0].BirthDate);
            Assert.NotNull(((result as GenericCommandResult)?.Data as List<Client>)?[0].RegisterDate);
        }

        [Fact]
        public async Task GetClientByCpfSucess()
        {
            // Arrange
            _mockClientRepository.GetByField(Arg.Any<Dictionary<object, object>>()).Returns(
                _listFakeClientEntity.Where(c => c.CnpjCpf.Equals("13586155000")).ToList());
            var command = new GetByNameCnpjCpfClientCommand(string.Empty, "13586155000");
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as GenericCommandResult)?.Success);
            Assert.Equal(_fakeClientEntity.Name, ((result as GenericCommandResult)?.Data as List<Client>)?[0].Name);
            Assert.Equal(_fakeClientEntity.CnpjCpf, ((result as GenericCommandResult)?.Data as List<Client>)?[0].CnpjCpf);
            Assert.Equal(_fakeClientEntity.BirthDate, ((result as GenericCommandResult)?.Data as List<Client>)?[0].BirthDate);
            Assert.NotNull(((result as GenericCommandResult)?.Data as List<Client>)?[0].RegisterDate);
        }

        [Fact]
        public async Task GetClientByCnpjSucess()
        {
            // Arrange
            _mockClientRepository.GetByField(Arg.Any<Dictionary<object, object>>()).Returns(
                _listFakeClientEntity.Where(c => c.CnpjCpf.Equals("67700676000169")).ToList());
            var command = new GetByNameCnpjCpfClientCommand(string.Empty, "67700676000169");
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as GenericCommandResult)?.Success);
            Assert.Equal(_fakeClientPJEntity.Name, ((result as GenericCommandResult)?.Data as List<Client>)?[0].Name);
            Assert.Equal(_fakeClientPJEntity.CnpjCpf, ((result as GenericCommandResult)?.Data as List<Client>)?[0].CnpjCpf);
            Assert.Equal(_fakeClientPJEntity.BirthDate, ((result as GenericCommandResult)?.Data as List<Client>)?[0].BirthDate);
            Assert.NotNull(((result as GenericCommandResult)?.Data as List<Client>)?[0].RegisterDate);
        }

        [Fact]
        public async Task GetClientByIdSucess()
        {
            // Arrange
            _mockClientRepository.GetById(Arg.Any<Guid>()).Returns(_fakeClientEntity);
            var command = new GetByIdClientCommand(Guid.NewGuid());
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.True((result as GenericCommandResult)?.Success);
            Assert.Equal(((result as GenericCommandResult)?.Data as Client)?.Name, _fakeClientEntity.Name);
            Assert.Equal(((result as GenericCommandResult)?.Data as Client)?.CnpjCpf, _fakeClientEntity.CnpjCpf);
            Assert.Equal(((result as GenericCommandResult)?.Data as Client)?.BirthDate, _fakeClientEntity.BirthDate);
        }

        [Fact]
        public async Task CreateClientNameCnpjCpfBirthDayNull()
        {
            // Arrange
            var command = new CreateClientCommand(string.Empty, string.Empty, DateTime.Now);
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ClientMessage.NameNotInforming, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
            Assert.Equal(ClientMessage.NameMaxCharacter, ((result as GenericCommandResult)?.Data as List<Notification>)?[1].Message);
            Assert.Equal(ClientMessage.CnpjCpfMaxCharacter, ((result as GenericCommandResult)?.Data as List<Notification>)?[2].Message);
            Assert.Equal(ClientMessage.CnpjCpfInvalid, ((result as GenericCommandResult)?.Data as List<Notification>)?[3].Message);
        }

        [Fact]
        public async Task CreateClientNameInvalid()
        {
            // Arrange
            var command = new CreateClientCommand("T", "13586155000", DateTime.Now);
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ClientMessage.NameNotInforming, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);

        }

        [Fact]
        public async Task CreateClientName246Characters()
        {
            // Arrange
            var command = new CreateClientCommand("ttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt" +
                "ttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt" +
                "tttttttttttttttttttttttttttttttttttttt", "13586155000", DateTime.Now);
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ClientMessage.NameMaxCharacter, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);

        }

        [Fact]
        public async Task CreateClientCpfInvalid()
        {
            // Arrange
            var command = new CreateClientCommand("Fake", "12345678901", DateTime.Now);
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ClientMessage.CnpjCpfInvalid, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task CreateClientCnpjInvalid()
        {
            // Arrange
            var command = new CreateClientCommand("Fake", "11111111000198", DateTime.Now);
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ClientMessage.CnpjCpfInvalid, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task UpdateClientNameInvalid()
        {
            // Arrange
            var command = new UpdateClientCommand(Guid.NewGuid(), "F");
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ClientMessage.NameNotInforming, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task UpdateClientName246Characters()
        {
            // Arrange
            var command = new UpdateClientCommand(Guid.NewGuid(), "ttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt" +
                "ttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt" +
                "tttttttttttttttttttttttttttttttttttttt");
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ClientMessage.NameMaxCharacter, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task UpdateClientNotFound()
        {
            // Arrange
            _mockClientRepository.GetById(Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")).Returns(_fakeClientEntity);
            var command = new UpdateClientCommand(Guid.NewGuid(), "Fake2");
            var handler = new ClientHandler(_mockClientRepository);
            
            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.False((result as GenericCommandResult)?.Success);
            Assert.Equal(ClientMessage.NotFound, (result as GenericCommandResult)?.Message);
        }

        [Fact]
        public async Task UpdateClientIdInvalid()
        {
            // Arrange
            var command = new UpdateClientCommand(Guid.Empty, "Fake");
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ClientMessage.IdInvalid, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task GetClientNameCnpjCpfError()
        {
            // Arrange
            var command = new GetByNameCnpjCpfClientCommand("Teste", "11111111111");
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ClientMessage.NameOrCnpjCpf, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task GetClientNameCnpjCpfEmpty()
        {
            // Arrange
            var command = new GetByNameCnpjCpfClientCommand(string.Empty, string.Empty);
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ClientMessage.NameOrCnpjCpf, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task GetClientCpfInvalid()
        {
            // Arrange
            var command = new GetByNameCnpjCpfClientCommand(string.Empty, "11111111111");
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ClientMessage.CnpjCpfInvalid, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task GetClientCnpjInvalid()
        {
            // Arrange
            var command = new GetByNameCnpjCpfClientCommand(string.Empty, "11111111000111");
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ClientMessage.CnpjCpfInvalid, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

        [Fact]
        public async Task GetClientIdInvalid()
        {
            // Arrange
            var command = new GetByIdClientCommand(Guid.Empty);
            var handler = new ClientHandler(_mockClientRepository);

            // Act
            var result = await handler.Handle(command);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ClientMessage.IdInvalid, ((result as GenericCommandResult)?.Data as List<Notification>)?[0].Message);
        }

    }
}