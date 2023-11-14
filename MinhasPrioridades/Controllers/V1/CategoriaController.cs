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
                return BadRequest(new { message = "Erro ao incluir registro " + ex.Message });
 
            }

        }

        [HttpGet()]
        public async Task<IActionResult> GetAll([FromQuery] string usuario_id)
        {
            try
            {
                var categorias = await _InterfaceCategoriaApp.ObterCategoria(usuario_id);

                return Ok(_mapper.Map<List<Domain.Prioridades.ViewModels.CategoriaViewModel>>(categorias));
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = "Erro ao retornar registros " + ex.Message });
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
                return BadRequest(new { message = "Erro ao retornar registro " + ex.Message } );
            }

        }


        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] CategoriaViewModel categoriaViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var categoria = _mapper.Map<Domain.Prioridades.Entities.Categoria>(categoriaViewModel);
                    await this._InterfaceCategoriaApp.UpdateCategoria(categoria);
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao alterar categoria " + ex.Message });
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                Categoria categoria = await _InterfaceCategoriaApp.GetEntityById(id);
                if (categoria != null)
                {
                    await this._InterfaceCategoriaApp.Delete(categoria);
                    return Ok();
                }
                else
                    return BadRequest(new { message = "Registro não encontrado"  });
               
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao excluir categoria. " + ex.Message  });
                
            }
        }

    }
}

