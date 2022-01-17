using ApplicationConsultaAPP.Interfaces;
using AutoMapper;
using ConsultaServer.Controllers.Filtros;
using Domain.Consulta.Entities;
using Domain.Consulta.Services;
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
    [Route("api/v{version:apiVersion}/usuario")]
    [ApiController]
    [ApiVersion("1.0")]
 
    public class UsuarioController : BaseController { 
        
        private readonly InterfaceUsuarioApp _InterfaceUsuarioApp;
        private readonly InterfaceAcessoApp _InterfaceAcessoApp;
        private readonly IMapper _mapper;

    public UsuarioController(InterfaceUsuarioApp interfaceUsuarioApp,
                             InterfaceAcessoApp interfaceAcessoApp,
                             IMapper mapper,
                             INotificador notificador) : base(notificador)
    {
        _InterfaceUsuarioApp = interfaceUsuarioApp;
        _InterfaceAcessoApp = interfaceAcessoApp;
        _mapper = mapper;
    }


    [HttpGet]
    [Route("obterporusuarioesenha")]
    [Authorize]
    public async Task<ActionResult<dynamic>> ObterPorUsuarioeSenha([FromQuery] string username,
                                                                   [FromQuery] string password)
    {

            try
            {
                var usuario = await _InterfaceUsuarioApp.ObterUsuario(username, password);
                             
                return Ok(usuario);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
          

        

    }

        [HttpPost]
        [Route("autenticar")]
        [AllowAnonymous]
        [ValidacaoModelStateCustomizado]
        public async Task<ActionResult<dynamic>> autenticar([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var usuario = await _InterfaceUsuarioApp.ObterUsuario(loginViewModel.Username, loginViewModel.Password);

                    if (usuario == null)
                        return NotFound("Usuário ou Senha inválidos" );

                    var token = TokenService.GenerateToken(usuario);
                    var acesso = await _InterfaceAcessoApp.ObterAcessoPorUsuario(usuario.Id);
                    return new
                    {
                        user = usuario,
                        acesso = acesso,
                        token = token,

                    };

                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
          

        }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            return Ok(await _InterfaceUsuarioApp.GetEntityById(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpGet()]
    [Route("obterporempresa/{empresa_id}")]
    [Authorize]
    public async Task<IActionResult> GetAll(Guid empresa_id)
    {
        try
        {
            return Ok(_mapper.Map<IEnumerable<LoginViewModel>>(await _InterfaceUsuarioApp.ObterPorEmpresa(empresa_id)));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

     

        [HttpPost]
    [AllowAnonymous]
    [ValidacaoModelStateCustomizado]
    public async Task<IActionResult> Add([FromBody] LoginViewModel loginViewModel)
    {
        try
        {

            if (ModelState.IsValid)
            {
                if (!_InterfaceUsuarioApp.IsUsuarioExiste(loginViewModel.Username))
                {
                   var usuario = await _InterfaceUsuarioApp.Incluir(loginViewModel);
                        var acesso = new AcessoViewModel() { Empresa_Id = loginViewModel.Empresa_Id, Usuario_Id = usuario.Id, Tipo = "S" };
                        await _InterfaceAcessoApp.Incluir(acesso);



                    return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, loginViewModel);
                }
                else
                    return BadRequest($"Usuário {loginViewModel.Username} já está cadastrado!");
            }
            return BadRequest();

        }
        catch (Exception ex)
        {
            return BadRequest("Erro ao incluir usuario " + ex.Message);
        }

    }


    [HttpPut()]
    [AllowAnonymous]
    [ValidacaoModelStateCustomizado]
    public async Task<IActionResult> Update([FromBody] LoginViewModel loginViewModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var usuario = _mapper.Map<Usuario>(loginViewModel);
                await this._InterfaceUsuarioApp.Alterar(usuario);
                return Ok();
            }
            return BadRequest();

        }
        catch (Exception ex)
        {
            return BadRequest("Erro ao alterar usuario " + ex.Message);
        }

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            Usuario usuario = await _InterfaceUsuarioApp.GetEntityById(id);
            if (usuario != null)
            {
                await this._InterfaceUsuarioApp.Delete(usuario);
                return Ok();
            }
            else
                return BadRequest("Registro não encontrado ");
        }
        catch (Exception ex)
        {

            return BadRequest("Erro ao excluir usuario " + ex.Message);
        }
     }


   }

}
