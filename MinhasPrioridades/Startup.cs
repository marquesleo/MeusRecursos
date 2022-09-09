using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinhasPrioridades.Extensions;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MinhasPrioridades
{7
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.WebConfig();
            services.AddCors();
            services.ConfigureJWT();
            services.ConfigureSwagger();
            services.Init(Configuration);
            services.ConfigureDependences(Configuration);
            services.AddAutoMapper(typeof(ApplicationPrioridadesAPP.AutoMapper.AutoMapperConfig));
            services.AddControllers().AddNewtonsoftJson();
                 JsonConvert.DefaultSettings = () => new JsonSerializerSettings
             {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
             };
            var setting = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
            



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //config swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prioridades Api");
                c.RoutePrefix = string.Empty; //swagger
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            //faz parte da autenticacao JWT
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseAuthentication();//parte do JWT
            app.UseAuthorization();
           

             app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
        }
    }
}
