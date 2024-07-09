using ApplicationPrioridadesAPP.Authorization;
using ApplicationPrioridadesAPP.Interfaces.Generics;
using AutoMapper;
using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Notification;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhasPrioridades.Controllers.V1
{
    [Route("api/v1/contadorsenha")]
    [ApiController]
    [Authorize]
    public class ContadorSenhaController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly InterfaceContadorSenhaApp _InterfaceContadorSenhaApp;
     
        public ContadorSenhaController(InterfaceContadorSenhaApp InterfaceContadorSenhaApp,
                          IMapper mapper,
                          INotificador notificador) : base(notificador)
        {
            _InterfaceContadorSenhaApp = InterfaceContadorSenhaApp;
            _mapper = mapper; 
            
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
