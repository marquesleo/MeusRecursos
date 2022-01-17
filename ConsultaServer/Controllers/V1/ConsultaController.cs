using ApplicationConsultaAPP.Interfaces;
using AutoMapper;
using ConsultaServer.Controllers.Filtros;
using Domain.Consulta.Entities;
using Domain.Consulta.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaServer.Controllers.V1
{
    [Route("api/v{version:apiVersion}/consulta")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class ConsultaController : BaseController
    {

        private readonly InterfaceConsultaApp _InterfaceConsultaApp;
   
        private readonly IMapper _mapper;

        public ConsultaController(InterfaceConsultaApp interfaceConsultaApp,
                                  IMapper mapper,
                                  INotificador notificador) : base(notificador)
        {
            _InterfaceConsultaApp = interfaceConsultaApp;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(_mapper.Map<ConsultaViewModel>( await _InterfaceConsultaApp.GetEntityById(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<ConsultaViewModel>>(await _InterfaceConsultaApp.List()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("validar")]
        public async Task<IActionResult> IsJaExisteMarcado([FromQuery] Guid empresa_id,[FromQuery] DateTime diaHora)
        {
            try
            {

                return Ok(await _InterfaceConsultaApp.IsJaExisteMarcado(empresa_id,diaHora));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ConsultarAgenda")]
        public async Task<IActionResult> ConsultarAgenda([FromQuery] Guid empresa_id,
                                                         [FromQuery] DateTime diaInicial,
                                                         [FromQuery] DateTime diaFinal,
                                                         [FromQuery] Guid paciente_id,
                                                         [FromQuery] Guid psicologo_id)
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<ConsultaViewModel>>(await _InterfaceConsultaApp.ConsultarAgenda(empresa_id, diaInicial,diaFinal,paciente_id,psicologo_id)));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ConsultarAgendaNaoRealizada")]
        public async Task<IActionResult> ConsultarAgendaNaoRealizada([FromQuery] Guid empresa_id,
                                                        [FromQuery] string dia,
                                                        [FromQuery] string paciente,
                                                        [FromQuery] string psicologa)
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<ConsultaViewModel>>(await _InterfaceConsultaApp.ConsultarAgendaNaoRealizada(empresa_id,dia,paciente,psicologa)));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [ValidacaoModelStateCustomizado]
        public async Task<IActionResult> Add([FromBody] ConsultaViewModel consultaViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    if (_InterfaceConsultaApp.IsJaExisteMarcado(consultaViewModel.Empresa_Id, consultaViewModel.Horario).Result)
                        return BadRequest("Ja Existe Consulta Marcada para esse horário!");

                    await _InterfaceConsultaApp.AddConsulta(consultaViewModel);
                    return CreatedAtAction(nameof(GetById), new { id = consultaViewModel.Id }, consultaViewModel);
                   
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao incluir consulta " + ex.Message);
            }

        }


        [HttpPut()]
        [ValidacaoModelStateCustomizado]
        public async Task<IActionResult> Update([FromBody] ConsultaViewModel consultaViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_InterfaceConsultaApp.IsJaExisteMarcado(consultaViewModel.Empresa_Id, consultaViewModel.Id, consultaViewModel.Horario).Result)
                        return BadRequest("Ja Existe Consulta Marcada para esse horário!");

                    await this._InterfaceConsultaApp.AlterarConsulta(consultaViewModel);
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao alterar consulta " + ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                Consulta consulta = await _InterfaceConsultaApp.GetEntityById(id);
                if (consulta != null)
                {
                    await this._InterfaceConsultaApp.Delete(consulta);
                    return Ok();
                }
                else
                    return BadRequest("Registro não encontrado ");
            }
            catch (Exception ex)
            {

                return BadRequest("Erro ao excluir consulta " + ex.Message);
            }
        }
    }
}
