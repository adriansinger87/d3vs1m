using D3vS1m.Application;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Infrastructure.Mqtt;
using D3vS1m.Domain.Runtime;
using D3vS1m.WebAPI.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Sin.Net.Domain.Persistence.Logging;
using System.IO;

namespace D3vS1m.WebAPI
{
    public class Startup
    {
        // -- fields

        private IHostingEnvironment _env;
        private IMqttControlable _mqtt;

        // -- constructor

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<FactoryBase>(new D3vS1mFactory());

            services.AddScoped<RuntimeBase>((s) =>
            {
                return new RuntimeController(new D3vS1mValidator());
            });

            // CORS
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin",
                                options => options.AllowAnyOrigin()
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod()
                );
            });


            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "DEVS1M", Version = "v1" }); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMqttControlable mqtt)
        {
            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            _env = env;

            _mqtt = mqtt;

            // error handling fowarded to ErrorController
            app.UseStatusCodePagesWithReExecute("/error");
            app.UseExceptionHandler("/error");

            if (_env.IsDevelopment())
            {
                app.UseHsts();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DEVS1M v1");
                    c.RoutePrefix = string.Empty;
                });


                //TODO: should be in Utils Class
                var path = Path.Combine(Directory.GetCurrentDirectory(), @"App_Data\ObjFiles");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                app.UseStaticFiles(new StaticFileOptions
                {
                    OnPrepareResponse = ctx =>
                    {
                        ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                        ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers",
                            "Origin, X-Requested-With, Content-Type, Accept");
                    },
                    ServeUnknownFileTypes = true,
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), @"App_Data\ObjFiles")),

                    RequestPath = new PathString("/data")
                });

                app.UseCors(options => options.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );
            }
            else
            {
                //TODO: prod env

                app.UseHttpsRedirection();
            }

            app.UseSignalR((routes) =>
            {
                routes.MapHub<ConsoleHub>("/hubs/console");
            });

            Log.Trace("server-app startup finished");
        }

        // -- event methods
    }
}