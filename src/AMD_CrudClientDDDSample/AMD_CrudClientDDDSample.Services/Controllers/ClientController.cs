using AMD_CrudClientDDDSample.Application.Command;
using AMD_CrudClientDDDSample.Application.Command.Client;
using AMD_CrudClientDDDSample.Application.Handler.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AMD_CrudClientDDDSample.Services.Controllers
{
    /// <summary>
    /// Controller de ações dos dados de cliente
    /// </summary>
    [Route("client")]
    [ApiController]
    public class ClientController : Controller
    {
        private IClientHandlerApplication _handle;

        /// <summary>
        /// Construtor com a implementação da application
        /// </summary>
        /// <param name="handle">Implementação do ClientHandlerApplication</param>
        public ClientController(IClientHandlerApplication handle) => _handle = handle;

        /// <summary>
        /// Criar um cliente
        /// </summary>
        /// <param name="command">Dados do Cliente: Nome, Cnpj ou CPF e Data de Nascimento ou Data de fundação</param>
        /// <returns>Dados inseridos do cliente</returns>
        [Authorize]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CommandResultModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateClientCommandApplication command)
        {
            try
            {
                var result = await _handle.Handle(command);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new CommandResultModel(false, ClientMessage.CreateError, ex.Message));
            };
        }

        /// <summary>
        /// Atualizar o nome do Cliente
        /// </summary>
        /// <param name="command">Dados do Cliente: Id e Nome</param>
        /// <returns>Dados atualizados do cliente</returns>
        [Authorize]
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CommandResultModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateClientCommandApplication command)
        {
            try
            {
                var result = await _handle.Handle(command);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                var commande = new CommandResultModel(false, ClientMessage.UpdateError, ex.Message);
                return BadRequest(commande);
            }
        }

        /// <summary>
        /// Buscar dados do cliente por nome ou cnpj/cpf
        /// </summary>
        /// <param name="name">Nome do cliente</param>
        /// <param name="cnpjCpf">Cnpj/Cpf do cliente</param>
        /// <returns>Dados do cliente</returns>
        [Authorize]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CommandResultModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByNameOrCnpjCpf([FromQuery]string? name, string? cnpjCpf)
        {
            try
            {
                var result = await _handle.Handle(new GetByNameCnpjCpfClientCommandApplication(name, cnpjCpf));
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                var commande = new CommandResultModel(false, ClientMessage.GetError, ex.Message);
                return BadRequest(commande);
            }
        }

        /// <summary>
        /// Buscar cliente pelo id
        /// </summary>
        /// <param name="id">Id do cliente</param>
        /// <returns>Dados do cliente</returns>
        [Authorize]
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CommandResultModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _handle.Handle(new GetByIdClientCommandApplication(id));
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                var commande = new CommandResultModel(false, ClientMessage.GetError, ex.Message);
                return BadRequest(commande);
            }
        }
    }
}