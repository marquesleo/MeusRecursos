using ApplicationPrioridadesAPP.Authorization;
using ApplicationPrioridadesAPP.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

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
            try
            {
                return Ok(await _InterfaceSenhaApp.GetSenhaById(id.ToString()));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

    }
}
