using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using ProjectAstra.Web.CrewApi.Core.Interfaces;
using ProjectAstra.Web.CrewApi.Core.Services;
using ProjectAstra.Web.CrewApi.Core.Validators;
using ProjectAstra.Web.CrewApi.Infrastructure.Data;
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
            // TODO: read about CORS and how to accept explicit test endpoints through it
            services.AddControllers().ConfigureApiBehaviorOptions(options => { }).AddNewtonsoftJson(t =>
            {
                t.SerializerSettings.MaxDepth = 128;
                t.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            ResolveDependencies(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
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
            services.AddScoped<IShuttleService, ShuttleService>();
            services.AddScoped<IShuttleRepository, ShuttleRepository>();
            services.AddSingleton<IShuttleValidator, ShuttleValidator>();
            
            services.AddScoped<ITeamOfExplorersService, TeamOfExplorersService>();
            services.AddScoped<ITeamOfExplorersRepository, TeamOfExplorersRepository>();
            services.AddSingleton<ITeamOfExplorersValidator, TeamOfExplorersValidator>();
            
            services.AddScoped<IHumanCaptainService, HumanCaptainService>();
            services.AddScoped<IHumanCaptainRepository, HumanCaptainRepository>();
            services.AddSingleton<IHumanCaptainValidator, HumanCaptainValidator>();
            
            services.AddScoped<IRobotService, RobotService>();
            services.AddScoped<IRobotRepository, RobotRepository>();
            services.AddSingleton<IRobotValidator, RobotValidator>();
            
            services.AddScoped<IExplorerRepository, ExplorerRepository>();
            
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
    }
}