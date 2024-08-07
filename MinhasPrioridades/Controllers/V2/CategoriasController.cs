﻿using ApplicationPrioridadesAPP.Authorization;
using Domain.Prioridades.ViewModels;
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
    public class CategoriasController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoriasController> _logger;
        public CategoriasController(IMediator mediator,
                                   ILogger<CategoriasController> logger)
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

            var query = new GetCategoriaQuery
            {
                Id = id
            };
            var res = await _mediator.Send(query);

            if (res.Success)
                return Ok(res.Data);
            else if (res.ErrorCode == ErrorCodes.CATEGORIA_NOT_FOUND)
                return NotFound(res);
            else
                return BadRequest(res);
        }


        [HttpGet()]
        public async Task<IActionResult> GetAll([FromQuery] string usuario_id)
        {
            try
            {
                var query = new GetAllCategoriaQuery
                {
                    Id_Usuario = Guid.Parse(usuario_id)

                };

                var res = await _mediator.Send(query);
                if (res.Success)
                    return Ok(res.Lista);
                else if (res.ErrorCode == ErrorCodes.CATEGORIA_NOT_FOUND)
                    return NotFound(res);
                else
                    return BadRequest(res);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = "Erro ao retornar registros " + ex.Message });
            }
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CategoriaViewModel categoriaViewModel)
        {

            if (categoriaViewModel.ImagemData != null )
            categoriaViewModel.ImagemData = categoriaViewModel.ImagemData.Substring(categoriaViewModel.ImagemData.LastIndexOf(',') + 1);

            var command = new UpdateCategoriaCommand
            {
                CategoriaViewModel = categoriaViewModel,
                Id = id
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

            var command = new DeleteCategoriaCommand
            {
                Id = id
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
