using ApplicationPrioridadesAPP.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using ApplicationPrioridadesAPP;
using ApplicationPrioridadesAPP.OpenApp.Senha.Queries;
using ApplicationPrioridadesAPP.Interfaces;
using Domain.Prioridades.ViewModels;
using ApplicationPrioridadesAPP.OpenApp.Prioridade.Command;
using ApplicationPrioridadesAPP.OpenApp.Senha.Command;

namespace MinhasPrioridades.Controllers.V2
{
    [Route("api/v2/Senha")]
    [ApiController]
    [Authorize]
    public class SenhaController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<SenhaController> _logger;
        public SenhaController(IMediator mediator,
                                   ILogger<SenhaController> logger)
        {

            _logger = logger;
            _mediator = mediator;
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetSenhaQuery
            {
                Id = id
            };
            var res = await _mediator.Send(query);

            if (res.Success)
                return Ok(res.Data);
            else if (res.ErrorCode == ErrorCodes.SENHA_NOT_FOUND)
                return NotFound(res);
            else
                return BadRequest(res);

        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SenhaViewModel senhaViewModel)
        {
            if (ModelState.IsValid)
            {

                var command = new CreateSenhaCommand
                {
                    SenhaViewModel = senhaViewModel
                };


                var res = await _mediator.Send(command);

                if (res.Success)
                {
                    return Created("", res.Data);
                }
                else
                {
                    return BadRequest(res);
                }
            }
            _logger.LogError("Response with unknown ErrorCode Returned");
            return BadRequest(500);

        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,
                                               [FromBody] SenhaViewModel senhaViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    senhaViewModel.Id = id;

                    await this._InterfaceSenhaApp.UpdateSenha(senhaViewModel);
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao alterar senha ." + ex.Message });
            }

        }


    }
}
