using ApplicationAPP.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhasPrioridades.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinhasSenhasController : ControllerBase
    {
        private readonly InterfaceMinhaSenhaApp _InterfaceMinhasSenhasApp;
        public MinhasSenhasController(InterfaceMinhaSenhaApp InterfaceMinhasSenhasApp)
        {

          this._InterfaceMinhasSenhasApp = InterfaceMinhasSenhasApp;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _InterfaceMinhasSenhasApp.GetEntityById(id));
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
            
        }


        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _InterfaceMinhasSenhasApp.List());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]  Entities.Models.MinhaSenha minhaSenha)
        {
            try
            {
                await _InterfaceMinhasSenhasApp.Add(minhaSenha);
                return CreatedAtAction(nameof(GetById), new { id = minhaSenha.Id }, minhaSenha);
            }
            catch (Exception ex)
            {
              return BadRequest("Erro ao incluir Minha Senha " + ex.Message);
            }
           
        }


        [HttpPut()]
      
        public async Task<IActionResult> Update([FromBody] Entities.Models.MinhaSenha minhaSenha)
        {
            try
            {
                await this._InterfaceMinhasSenhasApp.Add(minhaSenha);
                return Ok();
            }
            catch (Exception ex)
            {
                 return BadRequest("Erro ao alterar minhas senhas " +  ex.Message);
            }
                     
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                Entities.Models.MinhaSenha minhaSenha =  await _InterfaceMinhasSenhasApp.GetEntityById(id);
                if (minhaSenha != null)
                {
                    await this._InterfaceMinhasSenhasApp.Delete(minhaSenha);
                    return Ok();
                }
                else
                    return BadRequest("Registro não encontrado ");
            }
            catch (Exception ex)
            {

                return BadRequest("Erro ao excluir Minha Senha " + ex.Message);
            }
        }


    }
}
