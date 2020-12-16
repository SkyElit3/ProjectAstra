using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Services;
using ProjectAstra.Web.CrewApi.Infrastructure.DataContext;
using ProjectAstra.Web.CrewApi.Infrastructure.Repositories;
using ServerVersion = Pomelo.EntityFrameworkCore.MySql.Storage.ServerVersion;

namespace ProjectAstra.Web.CrewApi.Presentation
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
            services.AddControllers().ConfigureApiBehaviorOptions(options => { });
            services.AddScoped<IShuttleService, ShuttleService>();
            services.AddScoped<IShuttleRepo, ShuttleRepo>();
            
            services.AddDbContext<DataContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                    builderOptions =>
                    {
                        builderOptions.MigrationsAssembly("ProjectAstra.Web.CrewApi.Presentation")
                            .ServerVersion(new ServerVersion(new Version(5, 7, 12)))
                            .CharSet(CharSet.Latin1);
                    });
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<Startup> logger)
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
    }
}