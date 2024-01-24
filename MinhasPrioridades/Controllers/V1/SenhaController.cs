using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Notification;
using Domain.Prioridades.Entities;
using AutoMapper;
using ApplicationPrioridadesAPP.Interfaces;
using Domain.Prioridades.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace MinhasPrioridades.Controllers.V1
{
     [Route("api/v{version:apiVersion}/Senha")]
     [ApiController]
     [ApiVersion("1.0")]
     [Authorize]
    public class SenhaController : BaseController {

        private readonly IMapper _mapper;
        private readonly InterfaceSenhaApp _InterfaceSenhaApp;
        public SenhaController(IMapper mapper,
                               INotificador notificador,
                               InterfaceSenhaApp InterfaceSenhaApp) : base(notificador)
        {
            _InterfaceSenhaApp = InterfaceSenhaApp;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _InterfaceSenhaApp.GetSenhaById(id.ToString()));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]string usuario_id)
        {
            try
            {
               return Ok(await _InterfaceSenhaApp.ObterRegistros(usuario_id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ObterRegistrosPorUsuarioEDescricao")]
        public async Task<IActionResult> GetAllPorDescricaoEUsuario([FromQuery] string usuario_id, [FromQuery] string descricao )
        {
            try
            {
                return Ok(await _InterfaceSenhaApp.ObterRegistros(usuario_id,descricao));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SenhaViewModel senhaViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    await _InterfaceSenhaApp.AddSenha(senhaViewModel);
                    return CreatedAtAction(nameof(GetById), new { id = senhaViewModel.Id }, senhaViewModel);
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao incluir senha ." +   ex.Message });
            }

        }

          [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,
                                                 [FromBody] SenhaViewModel senhaViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    senhaViewModel.Id = id;
                   
                    await this._InterfaceSenhaApp.UpdateSenha(senhaViewModel);
                    return Ok();
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao alterar senha ." + ex.Message });
            }
         
        }
        

        

          [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                Senha senha = await _InterfaceSenhaApp.GetEntityById(id);
                if (senha != null)
                {
                    await this._InterfaceSenhaApp.Delete(senha);
                    return Ok();
                }
                else
                    return BadRequest(new { message = "Registro nao encontrado ." });
        
            }
            catch (Exception ex)
            {

               return BadRequest(new { message = ex.Message });
            }
        }



        [HttpGet]
        [Route("criptografartudo")]
        public async Task<IActionResult> CriptografarTudo()
        {
            try
            {

               
                   var foiCriptografado =  await  _InterfaceSenhaApp.CriptografarTudo();
                return Ok(foiCriptografado);
                
                

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao criptografar tudo ." + ex.Message });
            }

        }

    }
}