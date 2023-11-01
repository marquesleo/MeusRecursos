using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Notification;
using AutoMapper;
using ApplicationPrioridadesAPP.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;
using Microsoft.AspNetCore.Http;
using MiniValidation;

namespace MinhasPrioridades.Controllers.V1
{
    [Route("api/v{version:apiVersion}/usuario")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class UsuarioController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly InterfaceUsuarioApp _InterfaceUsuarioApp;
        private readonly IHttpContextAccessor _httpContextAccessor;
       
        public UsuarioController(InterfaceUsuarioApp interfaceUsuarioApp,
                                     IMapper mapper,
                                     INotificador notificador,
                                     IHttpContextAccessor httpContextAccessor) : base(notificador)
        {
            _InterfaceUsuarioApp = interfaceUsuarioApp;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
           
        }

        [AllowAnonymous]
        [Route("refresh-token")]
        [HttpPost()]
        public async Task<IActionResult> RefreshToken(RefreshTokenView refreshTokenView)
        {

            if (!MiniValidator.TryValidate(refreshTokenView, out var errors))
                return BadRequest(Results.ValidationProblem(errors));

           if (refreshTokenView != null && !string.IsNullOrEmpty(refreshTokenView.refreshtoken))
            {
                var response = await _InterfaceUsuarioApp.RefreshToken(refreshTokenView.accesstoken,
                                                                       refreshTokenView.refreshtoken,
                                                                       ipAddress());

                if (response == null)
                    return Unauthorized(new { message = "Invalid token" });

               

                return Ok(response);
            }
            else
                return Unauthorized(new { message = "Invalid token" });
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var response = await _InterfaceUsuarioApp.RevokeToken(token, ipAddress());

            if (!response)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }


        [HttpGet("{id}/refresh-tokens")]

        public async Task<IActionResult> GetRefreshTokens(Guid id)
        {
            var user = await _InterfaceUsuarioApp.GetEntityById(id);
            if (user == null) return NotFound();

            return Ok(user.RefreshTokens);
        }

        // helper methods


        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        [HttpPost]
        [Route("autenticar")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = await _InterfaceUsuarioApp.ObterUsuario(loginViewModel.Username,
                    loginViewModel.Password);


                    if (usuario == null || usuario.IsEmptyObject())
                        return BadRequest(new { message = "Usuário ou senha inválidos" });

                    var response = _InterfaceUsuarioApp.Authenticate(usuario, ipAddress());
                    
                    return Ok(response);

                 

                }
                else
                    return BadRequest(new { message = ModelState.ToString() });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
               
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
        [AllowAnonymous]
        public async Task<IActionResult> Add([FromBody] LoginViewModel loginViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (_InterfaceUsuarioApp.IsUsuarioExiste(loginViewModel.Username))
                        return BadRequest(new { message = $"Usuário {loginViewModel.Username} já está cadastrado!" });

                    if (_InterfaceUsuarioApp.IsUsuarioComEmailExistente(loginViewModel.Email))
                        return BadRequest(new { message = $"Usuário {loginViewModel.Email} está cadastrado!" });

                     await _InterfaceUsuarioApp.AddUsuario(loginViewModel);
                     return CreatedAtAction(nameof(GetById), new { id = loginViewModel.Id }, loginViewModel);
                    

                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao incluir usuario " + ex.Message });
                
            }

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    loginViewModel.Id = id;
                    var usuario = _mapper.Map<Usuario>(loginViewModel);

                    await this._InterfaceUsuarioApp.UpdateUsuario(loginViewModel);
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
                    return BadRequest(new { message = "Registro não encontrado" });
               
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao excluir usuario " + ex.Message });
               
            }
        }


    }
}
