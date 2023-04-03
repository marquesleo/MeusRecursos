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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Policy;

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
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenView refreshtoken)
        {
            //var refreshToken = Request.Cookies["refreshToken"];
            if (refreshtoken != null && !string.IsNullOrEmpty(refreshtoken.refreshtoken)) { 
                var response = await _InterfaceUsuarioApp.RefreshToken(refreshtoken.refreshtoken, ipAddress());

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });

            setTokenCookie(response.RefreshToken);

            return Ok(response);
         }else
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

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Domain = "localhost", //using https://localhost:44340/ here doesn't work
                Expires = DateTime.UtcNow.AddDays(14)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
            HttpContext.Response.Cookies.Append(
                     "refreshToken",token,
                     new CookieOptions() { SameSite = SameSiteMode.Lax });

        }
       
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
    try{
        if (ModelState.IsValid)
        {
            var usuario = await _InterfaceUsuarioApp.ObterUsuario(loginViewModel.Username,
            loginViewModel.Password);
            if (usuario == null || usuario.IsEmptyObject())
                return BadRequest(new { message = "Usuário ou senha inválidos" });
            var response = _InterfaceUsuarioApp.Authenticate(usuario, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);

            //    return Ok( new {
            //    user = new
            //    {
            //        Id = usuario.Id,
            //        Email = usuario.Email,
            //        Usename = usuario.Username
            //     },
            //    token = token,
            //    refreshToken = refreshToken

            //});

                }
            else
                return BadRequest();

          }catch(Exception ex){
            return StatusCode(500,ex.Message);
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
        public async Task<IActionResult> Add([FromBody] LoginViewModel loginViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (! _InterfaceUsuarioApp.IsUsuarioExiste(loginViewModel.Username))
                   { 
                        await _InterfaceUsuarioApp.AddUsuario(loginViewModel);
                        return CreatedAtAction(nameof(GetById), new { id = loginViewModel.Id }, loginViewModel);
                   }else
                    return BadRequest(new { message = $"Usuário {loginViewModel.Username} já está cadastrado!" });
                
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao incluir usuario " + ex.Message);
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
                    return BadRequest("Registro não encontrado ");
            }
            catch (Exception ex)
            {

                return BadRequest("Erro ao excluir usuario " + ex.Message);
            }
        }


    }
}
