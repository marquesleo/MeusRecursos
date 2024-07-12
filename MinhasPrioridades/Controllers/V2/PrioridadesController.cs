using ApplicationPrioridadesAPP.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using Domain.Prioridades.ViewModels;
using ApplicationPrioridadesAPP.OpenApp.Prioridade.Command;
using ApplicationPrioridadesAPP;
using ApplicationPrioridadesAPP.OpenApp.Prioridade.Queries;

namespace MinhasPrioridades.Controllers.V2
{
    [Route("api/v2/propriedades")]
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
            var query = new GetPrioridadeQuery
            {
                Id = id
            };
            var res = await _mediator.Send(query);

            if (res.Success)
                return Ok(res.Data);
            else if (res.ErrorCode == ErrorCodes.PRIORIDADE_NOT_FOUND)
                return NotFound(res);
            else
                return BadRequest(res);

        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string usuario_id, [FromQuery] bool? feito = false)
        {
            var query = new GetPrioridadeFiltrosQuery
            {
                Feito = feito,
                Usuario_Id = usuario_id
            };
            var res = await _mediator.Send(query);

            if (res.Success)
                return Ok(res.Lista);
            else if (res.ErrorCode == ErrorCodes.PRIORIDADE_NOT_FOUND)
                return NotFound(res);
            else
                return BadRequest(res);

        }



        [HttpPost("Up")]
        public async Task<IActionResult> Up([FromBody] PrioridadeViewModel prioridadeViewModel)
        {
           
                var command = new UpPrioridadeCommand
                {
                    PrioridadeViewModel = prioridadeViewModel
                };


                var res = await _mediator.Send(command);

                if (res.Success)
                {
                    return Ok(res.Data);
                }
                else
                {
                    return BadRequest(res);
                }
          
        }

        [HttpPost("Down")]
        public async Task<IActionResult> Down([FromBody] PrioridadeViewModel prioridadeViewModel)
        {
           
                var command = new DownPrioridadeCommand
                {
                    PrioridadeViewModel = prioridadeViewModel
                };
                var res = await _mediator.Send(command);

                if (res.Success)
                {
                    return Ok(res.Data);
                }
                else
                {
                    return BadRequest(res);
                }
         }


        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] PrioridadeViewModel prioridadeViewModel)
        {
          
                var command = new UpdatePrioridadeCommand
                {
                    PrioridadeViewModel = prioridadeViewModel
                };
            
                var res = await _mediator.Send(command);

                if (res.Success)
                {
                    return Ok(res.Data);
                }
                else
                {
                    return BadRequest(res);
                }
         }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeletePrioridadeCommand
            {
                 Id = id
            };

            var res = await _mediator.Send(command);

            if (res.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(res);
            }

        }
    }

}



