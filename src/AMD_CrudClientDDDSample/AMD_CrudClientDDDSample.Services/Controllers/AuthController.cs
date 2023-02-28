using AMD_CrudClientDDDSample.Application.Command;
using AMD_CrudClientDDDSample.Application.Command.Auth;
using AMD_CrudClientDDDSample.Application.Handler.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AMD_CrudClientDDDSample.Services.Controllers
{
    /// <summary>
    /// Token para autenticação
    /// </summary>
    [Route("auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthHandlerApplication _handle;

        /// <summary>
        /// Implementação da Application
        /// </summary>
        /// <param name="handle">Implementação da IAuthHandlerApplication </param>
        public AuthController(IAuthHandlerApplication handle)
        {
            _handle = handle;
        }

        /// <summary>
        /// Criar um token para o usuário
        /// </summary>
        /// <param name="commad">Dados do usuário : Nome e Password</param>
        /// <returns>Token para autenticação</returns>
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CommandResultModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateToken([FromBody] GetAuthCommandApplication commad)
        {
            try
            {
                var result = await _handle.Handle(commad);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new CommandResultModel(false, AuthMessage.AuthError, ex.Message));
            };
        }
    }
}