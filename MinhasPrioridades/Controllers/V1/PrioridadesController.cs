using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Notification;
using System.Collections.Generic;
using Domain.Prioridades.Entities;
using AutoMapper;
using ApplicationPrioridadesAPP.Interfaces;
using Domain.Prioridades.ViewModels;

namespace MinhasPrioridades.Controllers.V1
{
     [Route("api/v{version:apiVersion}/propriedades")]
     [ApiController]
     [ApiVersion("1.0")]
    public class PrioridadesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly InterfacePrioridadeApp _InterfacePrioridadeApp;
        public PrioridadesController(InterfacePrioridadeApp InterfacePrioridadeApp,
                                     IMapper mapper,
                                     INotificador notificador) : base(notificador)
        {
            _InterfacePrioridadeApp = InterfacePrioridadeApp;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _InterfacePrioridadeApp.GetEntityById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
       public async Task<IActionResult> GetAll([FromQuery]string usuario_id,[FromQuery] bool? feito=null)
        {
            try
            {
               return Ok(await _InterfacePrioridadeApp.ObterPrioridade(usuario_id,feito));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PrioridadeViewModel prioridadeViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var prioridade = new Prioridade();
                    prioridade.Map(prioridadeViewModel);
                    await _InterfacePrioridadeApp.AddPrioridade(prioridade);
                    return CreatedAtAction(nameof(GetById), new { id = prioridade.Id }, prioridade);
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao incluir Prioridade " + ex.Message);
            }

        }

        [HttpPost("Up")]
        public async Task<IActionResult> Up([FromBody] PrioridadeViewModel prioridadeViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var prioridade = new Prioridade();
                    prioridade.Map(prioridadeViewModel);
                    await _InterfacePrioridadeApp.Up(prioridade);
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao Up Prioridade " + ex.Message);
            }

        }

         [HttpPost("Down")]
        public async Task<IActionResult> Down([FromBody] PrioridadeViewModel prioridadeViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var prioridade = new Prioridade();
                    prioridade.Map(prioridadeViewModel);
                    await _InterfacePrioridadeApp.Down(prioridade);
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao Down Prioridade " + ex.Message);
            }

        }


        [HttpPut()]
       public async Task<IActionResult> Update([FromBody] PrioridadeViewModel prioridadeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var prioridade = new Prioridade();
                    prioridade.Map(prioridadeViewModel);
                    await this._InterfacePrioridadeApp.UpdatePrioridade(prioridade);
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao alterar prioridade " + ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                Prioridade prioridade = await _InterfacePrioridadeApp.GetEntityById(id);
                if (prioridade != null)
                {
                    await this._InterfacePrioridadeApp.Delete(prioridade);
                    return Ok();
                }
                else
                    return BadRequest("Registro não encontrado ");
            }
            catch (Exception ex)
            {

                return BadRequest("Erro ao excluir Prioridade " + ex.Message);
            }
        }
    }
}
