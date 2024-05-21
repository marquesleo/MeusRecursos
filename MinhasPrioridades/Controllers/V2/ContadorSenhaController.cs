using ApplicationPrioridadesAPP.Authorization;
using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using ApplicationPrioridadesAPP;
using ApplicationPrioridadesAPP.OpenApp.ContadorDeSenha.Queries;
using ApplicationPrioridadesAPP.OpenApp.ContadorDeSenha.Command;

namespace MinhasPrioridades.Controllers.V2
{
    [Route("api/v2/contadorsenha")]
    [ApiController]
    [Authorize]
    public class ContadorSenhaController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<ContadorSenhaController> _logger;
        public ContadorSenhaController(IMediator mediator,
                                   ILogger<ContadorSenhaController> logger)
        {

            _logger = logger;
            _mediator = mediator;
        }


        [HttpGet("{idSenha}")]
        public async Task<IActionResult> GetById(Guid idSenha)
        {
            var query = new GetContadorDeSenhaQuery
            {
                IdSenha = idSenha
            };
            var res = await _mediator.Send(query);

            if (res.Success)
                return Ok(res.Data);
            else if (res.ErrorCode == ErrorCodes.CATEGORIA_NOT_FOUND)
                return NotFound(res);
            else
                return BadRequest(res);

        }



        [HttpPost]
        public async Task<IActionResult> Atualizar([FromBody] ContadorSenhaViewModel contadorSenhaViewModel)
        {
            var command = new UpdateContadorCommand
            {
                ContadorSenhaViewModel = contadorSenhaViewModel
            };
            var res = await _mediator.Send(command);

            if (res.Success)
            {
                return Ok(res.Success);
            }
            else
            {
                return BadRequest(res);
            }

        }
    }
}
