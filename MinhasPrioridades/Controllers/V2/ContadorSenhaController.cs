using ApplicationPrioridadesAPP.Authorization;
using ApplicationPrioridadesAPP.Interfaces.Generics;
using AutoMapper;
using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Notification;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MinhasPrioridades.Controllers.V2
{
    [Route("api/v1/contadorsenha")]
    [ApiController]
    [Authorize]
    public class ContadorSenhaController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<ContadorSenhaController> _logger;
        public ContadorSenhaController(IMediator mediator,
                                   ILogger<ContadorSenhaController> logger)
        {

            _logger = logger;
            _mediator = mediator;
        }


        [HttpGet("{idSenha}")]
        public async Task<IActionResult> GetById(Guid idSenha)
        {
            try
            {
                var objeto = await _InterfaceContadorSenhaApp.GetContadorSenhaById(idSenha);
                var contadorDeSenha = _mapper.Map<List<ContadorSenhaViewModel>>(objeto);
                return Ok(contadorDeSenha);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpPost]
        public async Task<IActionResult> Atualizar([FromBody] ContadorSenhaViewModel contadorSenhaViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var contadorSenha = await _InterfaceContadorSenhaApp.GetContadorSenhaById(contadorSenhaViewModel.senhaId,
                                                                                              contadorSenhaViewModel.dtAcesso);
                    if (contadorSenha == null || contadorSenha.SenhaId == Guid.Empty)
                    {
                        var contador = new ContadorDeSenha();
                        contador.SenhaId = contadorSenhaViewModel.senhaId;
                        contador.Id = Guid.NewGuid();
                        contador.Contador = 1;
                        contador.DataDeAcesso = DateTime.Now;
                        //contador.Senha = await _InterfaceSenhaApp.GetEntityById(contadorSenhaViewModel.SenhaId);
                        await _InterfaceContadorSenhaApp.AddContador(contador);
                        return Ok();
                    }
                    else
                    {
                        contadorSenha.Contador += 1;

                        await _InterfaceContadorSenhaApp.UpdateSenha(contadorSenha);
                        return Ok();
                    }



                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao atualizar contador " + ex.Message });

            }

        }
    }
}
