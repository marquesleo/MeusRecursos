using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Notification;
using AutoMapper;
using ApplicationPrioridadesAPP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Domain.Prioridades.Services;
using System;
using System.Collections.Generic;
using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;

namespace MinhasPrioridades.Controllers.V1
{

    //[ApiVersion("1.0",Deprecated = true)] se for obsoleta
    [Route("api/v{version:apiVersion}/accounter")]
    [ApiController]
    [ApiVersion("1.0")]
    public class HomeController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly InterfaceUsuarioApp _InterfaceUsuarioApp;
        public HomeController(InterfaceUsuarioApp interfaceUsuarioApp,
                                     IMapper mapper,
                                     INotificador notificador) : base(notificador)
        {
            _InterfaceUsuarioApp = interfaceUsuarioApp;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {

                var usuario = await _InterfaceUsuarioApp.ObterUsuario(loginViewModel.Username, loginViewModel.Password);

                if (usuario == null)
                    return NotFound(new { messagem = "Usuário ou Senha inválidos" });

                var token = TokenService.GenerateToken(usuario);
                usuario.Password = "";
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

                    var usuario = _mapper.Map<Usuario>(loginViewModel);
                    await _InterfaceUsuarioApp.AddUsuario(usuario);
                    return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
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
                    await this._InterfaceUsuarioApp.UpdateUsuario(usuario);
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
                if (usuario != null) { 
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

