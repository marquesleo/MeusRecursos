using ApplicationPrioridadesAPP.Authorization;
using ApplicationPrioridadesAPP.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinhasPrioridades.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;



namespace MinhasPrioridades
{
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
           
            services.AddCors();
            services.ConfigureJWT();
            
            services.ConfigureSwagger();
            services.Init(Configuration);
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.ConfigureDependences(Configuration);
            //services.AddControllersWithViews()
            //  .AddJsonOptions(options =>
            //  options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;

                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.OnAppendCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            });


            services.AddAutoMapper(typeof(ApplicationPrioridadesAPP.AutoMapper.AutoMapperConfig));
            services.ConfigureAutoMapper();


            services.AddControllers()
            .AddJsonOptions(options =>
               options.JsonSerializerOptions.PropertyNamingPolicy = null);
               services.AddControllersWithViews().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddHttpClient();    
            JsonConvert.DefaultSettings = () =>
            {
             var settings = new JsonSerializerSettings
             {
                 ContractResolver = new CamelCasePropertyNamesContractResolver(),
                 PreserveReferencesHandling = PreserveReferencesHandling.None,
                 Formatting = Formatting.None
             };

             return settings;
         }; 
        }

        void CheckSameSite(HttpContext httpContext, CookieOptions options)
        {
            if (options.SameSite == SameSiteMode.None)
            {
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
                options.SameSite = SameSiteMode.Unspecified;
            }
        }

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

            //app.UseHttpsRedirection();

            app.UseRouting();

            //faz parte da autenticacao JWT
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseCookiePolicy();
            app.UseAuthentication();//parte do JWT
            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();
            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
        }
    }
}
