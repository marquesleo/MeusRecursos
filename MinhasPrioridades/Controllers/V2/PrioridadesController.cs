using ApplicationPrioridadesAPP.Authorization;
using ApplicationPrioridadesAPP.Interfaces;
using Domain.Prioridades.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using Domain.Prioridades.ViewModels;
using ApplicationPrioridadesAPP.OpenApp.Prioridade.Command;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MinhasPrioridades.Controllers.V2
{
    [Route("api/v2/prioridade")]
    [ApiController]
    [Authorize]
    public class PrioridadesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PrioridadesController> _logger;
        public PrioridadesController(IMediator mediator,
                                   ILogger<PrioridadesController> logger)
        {

            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PrioridadeViewModel prioridadeViewModel)
        {
            if (ModelState.IsValid)
            {

                var command = new CreatePrioridadeCommand
                {
                    PrioridadeViewModel = prioridadeViewModel
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
           

        }

    }
}


