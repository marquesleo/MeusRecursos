using ApplicationConsultaAPP.Interfaces;
using AutoMapper;
using ConsultaServer.Controllers.Filtros;
using Domain.Consulta.Entities;
using Domain.Consulta.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaServer.Controllers.V1
{
    [Route("api/v{version:apiVersion}/psicologa")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class PsicologaController : BaseController
    {
        private readonly InterfacePsicologaApp _InterfacePsicologaApp;
        private readonly IMapper _mapper;

        public PsicologaController(InterfacePsicologaApp interfacePsicologaApp,
                                     IMapper mapper,
                                     INotificador notificador) : base(notificador)
        {
            _InterfacePsicologaApp = interfacePsicologaApp;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(_mapper.Map<PsicologaViewModel>(await _InterfacePsicologaApp.GetEntityById(id)));
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
                return Ok(_mapper.Map<IEnumerable<PsicologaViewModel>>(await _InterfacePsicologaApp.List()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet()]
        [Route("obterporempresa/{id_empresa}")]
        public async Task<IActionResult> GetPacientesByEmpresa(Guid id_empresa)
        {
            try
            {
                var lstPsicologos = await _InterfacePsicologaApp.ObterPsicologos(id_empresa);
                return Ok(_mapper.Map<IEnumerable<PsicologaViewModel>>(lstPsicologos));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("obterporfiltros")]
        public async Task<IActionResult> ObterPorFiltros([FromQuery] string nomeDaPsicologa)
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<PsicologaViewModel>>(await _InterfacePsicologaApp.ObterPorNome(nomeDaPsicologa)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ValidacaoModelStateCustomizado]
        public async Task<IActionResult> Add([FromBody] PsicologaViewModel psicologaViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (!_InterfacePsicologaApp.IsPacienteExiste(psicologaViewModel.Nome, psicologaViewModel.Email, psicologaViewModel.Telefone))
                    {
                        await _InterfacePsicologaApp.Incluir(psicologaViewModel);
                        return CreatedAtAction(nameof(GetById), new { id = psicologaViewModel.Id }, psicologaViewModel);
                    }
                    else
                        return BadRequest($"Psicologa {psicologaViewModel.Nome} já está cadastrado!");
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao incluir psicologa " + ex.Message);
            }

        }


        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] PsicologaViewModel psicologaViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var psicologa = _mapper.Map<Psicologa>(psicologaViewModel);
                    await this._InterfacePsicologaApp.Alterar(psicologa);
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao alterar psicologa " + ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                Psicologa psicologa = await _InterfacePsicologaApp.GetEntityById(id);
                if (psicologa != null)
                {
                    await this._InterfacePsicologaApp.Delete(psicologa);
                    return Ok();
                }
                else
                    return BadRequest("Registro não encontrado ");
            }
            catch (Exception ex)
            {

                return BadRequest("Erro ao excluir psicologa " + ex.Message);
            }
        }


    }
}
