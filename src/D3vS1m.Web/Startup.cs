﻿using D3vS1m.Application;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace D3vS1m.Web
{
    public class Startup
    {
        // -- fields

        private IHostingEnvironment _env;

        public IConfiguration Configuration { get; }

        // -- constructor

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

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

            // HACK: re-work this approach to add a singleton for entire runtime
            var factory = new D3vS1mFactory(
                new RuntimeController(
                    new D3vS1mValidator()));

            services.AddSingleton<FactoryBase>(factory);
            // session
#if DEBUG
            TimeSpan ts = TimeSpan.FromSeconds(60);
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            _env = env;

            // error handling fowarded to ErrorController
            app.UseStatusCodePagesWithReExecute("/error");
            app.UseExceptionHandler("/error");

            if (_env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
