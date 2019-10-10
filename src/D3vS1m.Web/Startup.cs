using System.IO;
using D3vS1m.Application;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Infrastructure.Mqtt;
using D3vS1m.Infrastructure.Mqtt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Sin.Net.Domain.Persistence.Logging;
using D3vS1m.Domain.System.Extensions;
using D3vS1m.Application.Scene;
using Sin.Net.Persistence.Settings;
using D3vS1m.Persistence;
using System.Collections.Generic;
using D3vS1m.Application.Scene.Materials;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IMqttControlable, MqttNetController>();

            services.AddSingleton<FactoryBase>(ConfigureFactory());

            // CORS
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", 
                                options => options.AllowAnyOrigin()
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod()
                );
            });
            
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "DEVS1M", Version = "v1"}); });
        }

        private FactoryBase ConfigureFactory()
        {
            var factory = new D3vS1mFactory(
              new RuntimeController(
                  new D3vS1mValidator()));
            factory.GetPredefinedArguments();

            var setting = new JsonSetting
            {
                // TODO: remove magic strings
                Name = "material_config.json",
                Location = "App_Data",
                Binder = JsonHelper.Binder
            };

            var sceneArgs = factory.SimulationArguments.GetByName(D3vS1m.Application.Models.Scene.Key) as InvariantSceneArgs;

            sceneArgs.Materials = new IOController().Importer(Sin.Net.Persistence.Constants.Json.Key)
                .Setup(setting)
                .Import()
                .As<List<Material>>();

            return factory;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMqttControlable mqtt)
        {
            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            _env = env;

            _mqtt = mqtt;
            ConfigureMqtt();

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

                if (!Directory.Exists(path)) Directory.CreateDirectory(path);


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

            app.UseMvc();

            Log.Trace("server-app startup finished");
        }
        
        // TODO: should be moved to Utils 
        private void ConfigureMqtt()
        {
            _mqtt.Connected += (o, e) => { Log.Info($"Connected to '{e.Broker}' with id '{e.ClientID}'", this); };

            _mqtt.MessageReceived += (o, e) => { Log.Trace($"{e.Topic}: {e.Message}", this); };

            if (_mqtt.CreateClient(GetConfig()))
                _mqtt.ConnectAsync();
            else
                Log.Error("Client was not created", this);
        }

        private MqttConfig GetConfig()
        {
            return new MqttConfig
            {
                Broker = "broker.hivemq.com",
                Port = 1883,
                // TODO: setup config with client for each browser session or something like that
                // TODO: integrate client id in the topic to send data only to one browser
                ClientID = "d3vs1m-server-side",
                QoS = 2
            };
        }

        // -- event methods
    }
}