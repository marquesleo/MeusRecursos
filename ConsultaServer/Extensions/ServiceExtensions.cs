using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Notification;
using System.Reflection;
using System.IO;
using System;
using Microsoft.OpenApi.Models;
using Domain.Consulta.Interface;
using ApplicationConsultaAPP.Interfaces;
using Domain.Consulta.InterfaceService;
using Domain.Consulta.Services;
using Infrastructure.Consulta.Configuration;
using Infrastructure.Consulta.Repository.Generics;
using Domain.Consulta.Interfaces;
using Domain.Consulta.InterfaceServices;

namespace ConsultaServer.Extensions
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
            var key = Encoding.ASCII.GetBytes(Domain.Consulta.Settings.Secret);
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

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerGen(c =>
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using Bearer scheme (Example: 'Bearer 1234abcdef')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            }));
            services.AddSwaggerGen(c =>
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {

                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()

                }
           }));
        }


        public static void ConfigureDependences(this IServiceCollection services,
                                               IConfiguration configuration)
        {
            services.AddSingleton<INotificador, Notificacador>();

            services.AddSingleton(typeof(Contracts.Generics.IGeneric<>), typeof(RepositoryGeneric<>));
                 
            services.AddSingleton<IUsuario, Infrastructure.Consulta.Repository.Repositories.RepositoryUsuario>();
            services.AddSingleton<InterfaceUsuarioApp, ApplicationConsultaAPP.OpenApp.AppUsuario>();
            services.AddSingleton<IServiceUsuario, ServicesUsuario>();

            services.AddSingleton<IEmpresa, Infrastructure.Consulta.Repository.Repositories.RepositoryEmpresa>();
            services.AddSingleton<InterfaceEmpresaApp, ApplicationConsultaAPP.OpenApp.AppEmpresa>();
            services.AddSingleton<IServiceEmpresa, ServiceEmpresa>();

            services.AddSingleton<IAcesso, Infrastructure.Consulta.Repository.Repositories.RepositoryAcesso>();
            services.AddSingleton<InterfaceAcessoApp, ApplicationConsultaAPP.OpenApp.AppAcesso>();
            services.AddSingleton<IServiceAcesso, ServiceAcesso>();

            services.AddSingleton<IPaciente, Infrastructure.Consulta.Repository.Repositories.RepositoryPaciente>();
            services.AddSingleton<InterfacePacienteApp, ApplicationConsultaAPP.OpenApp.AppPaciente>();
            services.AddSingleton<IServicePaciente, ServicePaciente>();

            services.AddSingleton<IPsicologa, Infrastructure.Consulta.Repository.Repositories.RepositoryPsicologa>();
            services.AddSingleton<InterfacePsicologaApp, ApplicationConsultaAPP.OpenApp.AppPsicologa>();
            services.AddSingleton<IServicePsicologa, ServicePsicologa>();

            services.AddSingleton<IConsulta, Infrastructure.Consulta.Repository.Repositories.RepositoryConsulta>();
            services.AddSingleton<InterfaceConsultaApp, ApplicationConsultaAPP.OpenApp.AppConsulta>();
            services.AddSingleton<IServiceConsulta, ServiceConsulta>();

            services.AddDbContext<ContextBase>(p => p.UseNpgsql(GetStringConectionConfig(configuration)));
        }

        private static string GetStringConectionConfig(IConfiguration configuration)
        {
            return new MyDB(configuration).getStringConn().conexao;

        }
    }
}
