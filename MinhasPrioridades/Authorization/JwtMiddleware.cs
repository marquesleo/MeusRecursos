using System;
using ApplicationPrioridadesAPP.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using ApplicationPrioridadesAPP.Interfaces;
using System.Linq;

namespace MinhasPrioridades.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly InterfaceUsuarioApp _interfaceUsuarioApp;
        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings,
            InterfaceUsuarioApp interfaceUsuarioApp)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _interfaceUsuarioApp = interfaceUsuarioApp;
        }

        public async Task Invoke(HttpContext context, InterfaceUsuarioApp userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = await userService.GetEntityById(Guid.Parse( userId.Value.ToString()));
            }

            await _next(context);
        }
    }
    

}

