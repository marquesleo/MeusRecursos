using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeusRecursos.Extensions
{
    public static class ServiceExtensions
    {

        public static void Init(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(provider => configuration);
            services.AddSingleton<Infrastructure.Configuration.MyDB>();
        }
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddSingleton(typeof(Contracts.Generics.IGeneric<>), typeof(Infrastructure.Repository.Generics.RepositoryGeneric<>));
            //services.AddSingleton<IUsuario, RepositoryUsuario>();
            //services.AddSingleton<ApplicationAPP.Interfaces.InterfaceUsuarioApp, ApplicationPrioridadesAPP.OpenApp.AppUsuario>();
        }
    }
}
