using ApplicationPrioridadesAPP.OpenApp.Usuario.Command;
using ApplicationPrioridadesAPP.OpenApp.Usuario.Queries;
using Domain.Prioridades.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MinhasPrioridades.Controllers.V2
{
    [Route("api/v2/usuario")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsuarioController> _logger;
        public UsuarioController(IMediator mediator,
                                   ILogger<UsuarioController> logger)
        {

            _logger = logger;
            _mediator = mediator;
        }


        [HttpPost]
        [Route("autenticar")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] LoginViewModel loginViewModel)
        {

            var query = new GetAutenticacaoQuery
            {
                Login = loginViewModel
            };

            var res =  await _mediator.Send(query);

            if (res.ErrorCode == ApplicationPrioridadesAPP.ErrorCodes.USUARIO_NOT_FOUND)
            {
                return BadRequest(res);
            } else if (res.Success)
            {
                return Ok(res.Data);
            } else
                return BadRequest(res);
            
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetUsuarioQuery
            {
                 Id = id
            };

            var res = await _mediator.Send(query);

            if (res.ErrorCode == ApplicationPrioridadesAPP.ErrorCodes.USUARIO_NOT_FOUND)
            {
                return BadRequest(res);
            }
            else if (res.Success)
            {
                return Ok(res.Data);
            }
            else
                return BadRequest(res);

        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllUsuarioQuery
            {
               
            };

            var res = await _mediator.Send(query);

            if (res.ErrorCode == ApplicationPrioridadesAPP.ErrorCodes.USUARIO_NOT_FOUND)
            {
                return BadRequest(res);
            }
            else if (res.Success)
            {
                return Ok(res.Lista);
            }
            else
                return BadRequest(res);
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Add([FromBody] LoginViewModel loginViewModel)
        {


            var command = new CreateUsuarioCommand
            {
                LoginViewModel = loginViewModel
            };
            var res = await _mediator.Send(command);

            if (res.ErrorCode == ApplicationPrioridadesAPP.ErrorCodes.USUARIO_DUPLICATE || res.ErrorCode == ApplicationPrioridadesAPP.ErrorCodes.USUARIO_COM_EMAIL_EXISTENTE)
            {
                return BadRequest(res);
            }
            else if (res.Success)
            {
                return Ok(res.Data);
            }
            else
                return BadRequest(res);
            
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] LoginViewModel loginViewModel)
        {
            var command = new UpdateUsuarioCommand
            {
                LoginViewModel = loginViewModel,
                Id = id
            };
            var res = await _mediator.Send(command);

            if (res.ErrorCode == ApplicationPrioridadesAPP.ErrorCodes.USUARIO_DUPLICATE || res.ErrorCode == ApplicationPrioridadesAPP.ErrorCodes.USUARIO_COM_EMAIL_EXISTENTE)
            {
                return BadRequest(res);
            }
            else if (res.Success)
            {
                return Ok(res.Data);
            }
            else
                return BadRequest(res);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteUsuarioCommand
            {
                Id = id
            };
            var res = await _mediator.Send(command);

            if (res.ErrorCode == ApplicationPrioridadesAPP.ErrorCodes.USUARIO_NOT_FOUND )
            {
                return BadRequest(res);
            }
            else if (res.Success)
            {
                return Ok();
            }
            else
                return BadRequest(res);
        }

    }
}
