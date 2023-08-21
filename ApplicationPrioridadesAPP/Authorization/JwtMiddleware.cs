using System;
using ApplicationPrioridadesAPP.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Linq;
using Domain.Prioridades.Interface;

namespace ApplicationPrioridadesAPP.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly IUsuario _IUsuario;
        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings,
            IUsuario IUsuario)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _IUsuario = IUsuario;
        }

        public async Task Invoke(HttpContext context, IUsuario userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = await userService.GetEntityById(Guid.Parse( userId.ToString()));
            }

            await _next(context);
        }
    }
    

}

