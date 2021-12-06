using ApplicationConsultaAPP.Interfaces;
using AutoMapper;
using Domain.Consulta.Entities;
using Domain.Consulta.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaServer.Controllers.V1
{
    [Route("api/v{version:apiVersion}/paciente")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PacienteController : BaseController
    {

        private readonly InterfacePacienteApp _InterfacePacienteApp;
        private readonly IMapper _mapper;

        public PacienteController(InterfacePacienteApp interfacePacienteApp,
                                     IMapper mapper,
                                     INotificador notificador) : base(notificador)
        {
            _InterfacePacienteApp = interfacePacienteApp;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _InterfacePacienteApp.GetEntityById(id));
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
                return Ok(_mapper.Map<IEnumerable<PacienteViewModel>>(await _InterfacePacienteApp.List()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PacienteViewModel pacienteViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!_InterfacePacienteApp.IsPacienteExiste(pacienteViewModel.Nome,pacienteViewModel.Email,pacienteViewModel.Telefone))
                    {
                        await _InterfacePacienteApp.Incluir(pacienteViewModel);
                        return CreatedAtAction(nameof(GetById), new { id = pacienteViewModel.Id }, pacienteViewModel);
                    }
                    else
                        return BadRequest($"Usuário {pacienteViewModel.Nome} já está cadastrado!");
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao incluir usuario " + ex.Message);
            }

        }


        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] PacienteViewModel pacienteViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var paciente = _mapper.Map<Paciente>(pacienteViewModel);
                    await this._InterfacePacienteApp.Alterar(paciente);
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao alterar paciente " + ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                Paciente paciente = await _InterfacePacienteApp.GetEntityById(id);
                if (paciente != null)
                {
                    await this._InterfacePacienteApp.Delete(paciente);
                    return Ok();
                }
                else
                    return BadRequest("Registro não encontrado ");
            }
            catch (Exception ex)
            {

                return BadRequest("Erro ao excluir paciente " + ex.Message);
            }
        }


    }
}
