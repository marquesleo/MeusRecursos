using ApplicationPrioridadesAPP.Authorization;
using ApplicationPrioridadesAPP.Interfaces.Generics;
using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notification;
using System;
using System.Threading.Tasks;

namespace MinhasPrioridades.Controllers.V1
{
    [Route("api/v{version:apiVersion}/contadorsenha")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class ContadorSenhaController : BaseController
    {
      
        private readonly InterfaceContadorSenhaApp _InterfaceContadorSenhaApp;
        public ContadorSenhaController(InterfaceContadorSenhaApp InterfaceContadorSenhaApp,
             INotificador notificador) : base(notificador)
        {
            _InterfaceContadorSenhaApp = InterfaceContadorSenhaApp;

            
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar([FromBody] ContadorSenhaViewModel contadorSenhaViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                   
                    var contadorSenha = await _InterfaceContadorSenhaApp.GetContadorSenhaById(contadorSenhaViewModel.SenhaId,
                                                                                              contadorSenhaViewModel.dtAcesso);
                    if (contadorSenha == null || contadorSenha.SenhaId == Guid.Empty)
                    {
                        var contador = new ContadorDeSenha();
                        contador.SenhaId = contadorSenhaViewModel.SenhaId;
                        contador.Contador = 1;
                        contador.DataDeAcesso = DateTime.Now;
                        await _InterfaceContadorSenhaApp.AddContador(contadorSenha);
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
