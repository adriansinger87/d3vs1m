using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;

namespace D3vS1m.WebApi.GraphQL.Types
{
	public class WeatherType : ObjectGraphType<WeatherForecast>
	{
		public WeatherType()
		{
			Field(x => x.TemperatureC).Description("The temperature in celsius.");
			Field(x => x.Summary).Description("The summary in one word.");
		}
	}
}
