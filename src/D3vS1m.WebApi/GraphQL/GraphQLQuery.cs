using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D3vS1m.WebApi.GraphQL.Types;
using GraphQL.Types;

namespace D3vS1m.WebApi.GraphQL
{
	public class GraphQLQuery : ObjectGraphType
	{
		public GraphQLQuery()
		{
			Field<ListGraphType<WeatherType>>("WeatherForecasts", resolve: context => Get());
		}

		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};


		public IEnumerable<WeatherForecast> Get()
		{
			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			})
			.ToArray();
		}
	}
}
