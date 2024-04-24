using ApplicationPrioridadesAPP.Authorization;
using ApplicationPrioridadesAPP.Interfaces;
using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using MediatR;
using Microsoft.Extensions.Logging;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Command;

namespace MinhasPrioridades.Controllers.V2
{
    [Route("api/v2/categoria")]
    [ApiController]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoriaController> _logger;
        public CategoriaController(IMediator mediator,
                                   ILogger<CategoriaController> logger)
        {
         
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoriaViewModel categoriaViewModel)
        {
           
            if (ModelState.IsValid)
              {

                    var command = new CreateCategoriaCommand
                    {
                        CategoriaViewModel = categoriaViewModel
                    };


                    var res = await _mediator.Send(command);

                    if (res.Success)
                    {
                        return Created("", res.Data);
                    }else
                    {
                        return BadRequest(res);
                    }
             }
            

            _logger.LogError("Response with unknown ErrorCode Returned");
            return BadRequest(500);

        }
    }
}
