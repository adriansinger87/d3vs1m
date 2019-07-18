using System;
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
using Sin.Net.Domain.Persistence.Logging;

namespace D3vS1m.Web
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IMqttControlable, MqttNetController>();

            var factory = new D3vS1mFactory(
                new RuntimeController(
                    new D3vS1mValidator()));
            services.AddSingleton<FactoryBase>(factory);

            // CORS
            services.AddCors(c => { c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()); });


            // session
#if DEBUG
            var ts = TimeSpan.FromSeconds(60);
#else
            TimeSpan ts = TimeSpan.FromMinutes(10);
#endif

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = ts;
                options.Cookie.HttpOnly = true;
            });
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

            if (_env.IsDevelopment()) app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            // TODO: Need to be handled by Dev or Prod Env
            app.UseCors(options => options.AllowAnyOrigin());   


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

            Log.Trace("server-app startup finished");
        }

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