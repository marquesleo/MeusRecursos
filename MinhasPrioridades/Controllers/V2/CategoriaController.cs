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
using ApplicationPrioridadesAPP;
using ApplicationPrioridadesAPP.OpenApp.Categoria.Queries;

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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {

            var query = new GetCategoriaQuery
            {
                Id = id
            };
            var res = await _mediator.Send(query);

            if (res.Success)
                return Created("", res.Data);
            else if (res.ErrorCode == ErrorCodes.CATEGORIA_NOT_FOUND)
                return NotFound(res);
            else
                return BadRequest(res);
        }
    }
    
}
