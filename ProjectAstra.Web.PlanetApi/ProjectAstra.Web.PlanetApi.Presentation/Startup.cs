using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using ProjectAstra.Web.PlanetApi.Core.Interfaces;
using ProjectAstra.Web.PlanetApi.Core.Services;
using ProjectAstra.Web.PlanetApi.Core.Validators;
using ProjectAstra.Web.PlanetApi.Infrastructure.Data;
using ProjectAstra.Web.PlanetApi.Infrastructure.Repositories;

namespace ProjectAstra.Web.PlanetApi.Presentation
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().ConfigureApiBehaviorOptions(options => { }).AddNewtonsoftJson(t =>
            {
                t.SerializerSettings.MaxDepth = 128;
                t.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddHttpClient();
            ResolveDependencies(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
                endpoints.MapDefaultControllerRoute();
            });
        }
        private void ResolveDependencies(IServiceCollection services)
        {
            services.AddScoped<ISolarSystemService, SolarSystemService>();
            services.AddScoped<ISolarSystemRepository, SolarSystemRepository>();
            services.AddSingleton<ISolarSystemValidator, SolarSystemValidator>();
            
            services.AddScoped<IPlanetService, PlanetService>();
            services.AddScoped<IPlanetRepository, PlanetRepository>();
            services.AddSingleton<IPlanetValidator, PlanetValidator>();
            
            services.AddDbContext<DataContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                    builderOptions =>
                    {
                        builderOptions.MigrationsAssembly("ProjectAstra.Web.PlanetApi.Presentation")
                            .ServerVersion(new ServerVersion(new Version(5, 7, 12)))
                            .CharSet(CharSet.Latin1);
                    });
            });
        }
    }
}