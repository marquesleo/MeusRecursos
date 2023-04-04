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
using System.Reflection;
using System.IO;
using System;
using Microsoft.OpenApi.Models;
using Domain.Prioridades.Interfaces;
using Microsoft.AspNetCore.Http;
using ApplicationPrioridadesAPP.Authorization;

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
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(key),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
             })
             .AddCookie("Cookies", options =>
             {
                 options.LoginPath = "/login";
                 options.ExpireTimeSpan = TimeSpan.FromDays(7);
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
       
            services.AddSingleton(typeof(Contracts.Generics.IGeneric<>), typeof(Infrastructure.Repository.Generics.RepositoryGeneric<>));
            services.AddSingleton<IPrioridade, Infrastructure.Repository.Repositories.RepositoryPrioridade>();
            services.AddSingleton<InterfacePrioridadeApp, ApplicationPrioridadesAPP.OpenApp.AppPrioridade>();
            services.AddSingleton<IServicePrioridade, ServicesPrioridade>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ISenha, Infrastructure.Repository.Repositories.RepositoryMinhaSenha>();
            services.AddSingleton<IRefreshToken, Infrastructure.Repository.Repositories.RepositoryRefreshToken>();
            services.AddSingleton<InterfaceSenhaApp, ApplicationPrioridadesAPP.OpenApp.AppSenha>();
            services.AddSingleton<IServiceSenha, ServicesSenha>();
           
            services.AddSingleton<IUsuario, Infrastructure.Repository.Repositories.RepositoryUsuario>();
            services.AddSingleton<InterfaceUsuarioApp, ApplicationPrioridadesAPP.OpenApp.AppUsuario>();
            services.AddSingleton<IServiceUsuario, ServicesUsuario>();
            services.AddSingleton<IJwtUtils, JwtUtils>();
            services.AddDbContext<ContextBase>(p => p.UseNpgsql(GetStringConectionConfig(configuration)));
        }

        private static string GetStringConectionConfig(IConfiguration configuration)
        {
            return new MyDB(configuration).getStringConn().conexao;

        }
    }
}
