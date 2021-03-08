using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.WebApi.GraphQL.Schemas;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace D3vS1m.WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddScoped<RuntimeBase>(ConfigureSimulation);

			/*
			 * GraphQL tutorial: https://code-maze.com/graphql-asp-net-core-tutorial/
			 */
			services.AddScoped<ApiSchema>();
			services.AddGraphQL()
				.AddSystemTextJson()
				.AddGraphTypes(typeof(ApiSchema), ServiceLifetime.Scoped);

			services.AddControllers();
		}

		// Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseGraphQL<ApiSchema>();
			app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	
		// -- helper
	
		private RuntimeBase ConfigureSimulation(IServiceProvider provider)
		{
			var runtime = new RuntimeController(new D3vS1mValidator());

			runtime.SetupSimulators((repo) =>
			{
				//repo[SimulationTypes.Antenna].With(SimArgs[SimulationTypes.Network]);
				//repo[SimulationTypes.Energy].With(SimArgs[SimulationTypes.Network]);
			});

			runtime.Stopped += (o, e) =>
			{
				// done!
			};

			runtime.IterationPassed += (o, e) =>
			{
				// iteration done!
			};

			return runtime;
		}
	}
}
