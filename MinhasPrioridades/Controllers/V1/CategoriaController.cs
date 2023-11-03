using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationPrioridadesAPP.Authorization;
using ApplicationPrioridadesAPP.Interfaces;
using AutoMapper;
using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Notification;

namespace MinhasPrioridades.Controllers.V1
{
    [Route("api/v{version:apiVersion}/categoria")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class CategoriaController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly InterfaceCategoriaApp _InterfaceCategoriaApp;
        public CategoriaController(IMapper mapper,
                                   InterfaceCategoriaApp InterfaceCategoriaApp,
                                    INotificador notificador) : base(notificador)
        {
            this._InterfaceCategoriaApp = InterfaceCategoriaApp;
            this._mapper = mapper;
		}

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoriaViewModel categoriaViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                 var  categoria = _mapper.Map<Domain.Prioridades.Entities.Categoria>(categoriaViewModel);
                 await _InterfaceCategoriaApp.AddCategoria(categoria);

                 return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao incluir Prioridade " + ex.Message);
            }

        }

        [HttpGet()]
        [Route("obtertodos")]
        public async Task<IActionResult> GetAll([FromQuery] string usuario_id)
        {
            try
            {
                var categorias = await _InterfaceCategoriaApp.ObterCategoria(usuario_id);

                return Ok(_mapper.Map<List<Domain.Prioridades.ViewModels.CategoriaViewModel>>(categorias));
            }
            catch (Exception ex)
            {
              
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _InterfaceCategoriaApp.GetEntityById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}

