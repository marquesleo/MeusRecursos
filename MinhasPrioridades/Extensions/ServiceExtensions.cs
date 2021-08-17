using Domain.Prioridades.Interface;
using ApplicationPrioridadesAPP.Interfaces;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Domain.Prioridades.InterfaceService;
using Domain.Prioridades.Services;
using Notification;
using AutoMapper;

namespace MinhasPrioridades.Extensions
{
    public static class ServiceExtensions
    {

        public static void Init(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(provider => configuration);
            services.AddSingleton<MyDB>();
        }

        public static void ConfigureJWT(this IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Domain.Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });
        }
        public static void ConfigureDependences(this IServiceCollection services,
                                               IConfiguration configuration)
        {
            services.AddSingleton<INotificador, Notificacador>();

            services.AddSingleton(typeof(Contracts.Generics.IGeneric<>), typeof(Infrastructure.Repository.Generics.RepositoryGeneric<>));
            services.AddSingleton<IPrioridade, Infrastructure.Repository.Repositories.RepositoryPrioridade>();
            services.AddSingleton<InterfacePrioridadeApp, ApplicationPrioridadesAPP.OpenApp.AppPrioridade>();
            services.AddSingleton<IServicePrioridade, ServicesPrioridade>();
           
            services.AddSingleton<IUsuario, Infrastructure.Repository.Repositories.RepositoryUsuario>();
            services.AddSingleton<InterfaceUsuarioApp, ApplicationPrioridadesAPP.OpenApp.AppUsuario>();
            services.AddSingleton<IServiceUsuario, ServicesUsuario>();

            services.AddDbContext<ContextBase>(p => p.UseNpgsql(GetStringConectionConfig(configuration)));
        }

        private static string GetStringConectionConfig(IConfiguration configuration)
        {
            return new MyDB(configuration).getStringConn().conexao;

        }
    }
}
