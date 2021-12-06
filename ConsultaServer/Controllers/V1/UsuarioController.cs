using ApplicationConsultaAPP.Interfaces;
using AutoMapper;
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
        private readonly IMapper _mapper;

    public UsuarioController(InterfaceUsuarioApp interfaceUsuarioApp,
                                 IMapper mapper,
                                 INotificador notificador) : base(notificador)
    {
        _InterfaceUsuarioApp = interfaceUsuarioApp;
        _mapper = mapper;
    }


    [HttpPost]
    [Route("autenticar")]
    [AllowAnonymous]
    public async Task<ActionResult<dynamic>> Authenticate([FromBody] LoginViewModel loginViewModel)
    {
        if (ModelState.IsValid)
        {

            var usuario = await _InterfaceUsuarioApp.ObterUsuario(loginViewModel.Username, loginViewModel.Password);

            if (usuario == null)
                return NotFound(new { messagem = "Usuário ou Senha inválidos" });

            var token = TokenService.GenerateToken(usuario);

            return new
            {
                user = usuario,
                token = token
            };

        }
        else
            return BadRequest();

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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(_mapper.Map<IEnumerable<LoginViewModel>>(await _InterfaceUsuarioApp.List()));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] LoginViewModel loginViewModel)
    {
        try
        {

            if (ModelState.IsValid)
            {
                if (!_InterfaceUsuarioApp.IsUsuarioExiste(loginViewModel.Username))
                {
                    await _InterfaceUsuarioApp.Incluir(loginViewModel);
                    return CreatedAtAction(nameof(GetById), new { id = loginViewModel.Id }, loginViewModel);
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
