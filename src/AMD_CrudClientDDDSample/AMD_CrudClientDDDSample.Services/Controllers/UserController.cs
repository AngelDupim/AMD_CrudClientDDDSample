using AMD_CrudClientDDDSample.Application.Command;
using AMD_CrudClientDDDSample.Application.Command.User;
using AMD_CrudClientDDDSample.Application.Handler.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AMD_CrudClientDDDSample.Services.Controllers
{
    /// <summary>
    /// Controller de ações dos dados de usuário
    /// </summary>
    [Route("user")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserHendlerApplication _handle;

        /// <summary>
        /// Implementação da Application
        /// </summary>
        /// <param name="handle">Implementação do IUserHendlerApplication</param>
        public UserController(IUserHendlerApplication handle)
        {
            _handle = handle;
        }

        /// <summary>
        /// Criar um usuário
        /// </summary>
        /// <param name="commad">Dados do usuário : Nome, Password e Cnpj/Cpf</param>
        /// <returns>Dados do usuário atualizado</returns>
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CommandResultModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateUserCommandApplication commad)
        {
            try
            {
                var result = await _handle.Handle(commad);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new CommandResultModel(false, UserMessage.CreateError, ex.Message));
            };
        }

        /// <summary>
        /// Desativar o usuário
        /// </summary>
        /// <param name="commad">Id do usuário</param>
        /// <returns>Dados atualizado do usuário</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CommandResultModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete([FromBody] DeleteUserCommandApplication commad)
        {
            try
            {
                var result = await _handle.Handle(commad);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new CommandResultModel(false, UserMessage.DeleteError, ex.Message));
            };
        }

        /// <summary>
        /// Buscar dados do usuário pelo nome ou cnpj/cpf
        /// </summary>
        /// <param name="name">Nome do usuário</param>
        /// <param name="cnpjCpf">Cpf/Cnpj do usuário</param>
        /// <returns>Dados do usuário</returns>
        //[Authorize]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CommandResultModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByUserNameCnpjCpf([FromQuery] string? name, string? cnpjCpf )
        {
            try
            {
                var result = await _handle.Handle(new GetByNameCnpjCpfUserCommandApplication(name, cnpjCpf));
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new CommandResultModel(false, UserMessage.GetError, ex.Message));
            };
        }

        /// <summary>
        /// Busar o usuário pelo Id
        /// </summary>
        /// <param name="command">Id do usuário</param>
        /// <returns>Dados do usuário</returns>
        [Authorize]
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CommandResultModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByIdUser(Guid id)
        {
            try
            {
                var result = await _handle.Handle(new GetByIdUserCommandApplication(id));
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new CommandResultModel(false, UserMessage.GetError, ex.Message));
            };
        }
    }
}