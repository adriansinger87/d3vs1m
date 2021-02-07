using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using GraphQL.Utilities;

namespace D3vS1m.WebApi.GraphQL.Schemas
{
	public class ApiSchema : Schema
	{
		public ApiSchema(IServiceProvider provider) : base(provider)
		{
			Query = provider.GetRequiredService<GraphQLQuery>();
		}
	}
}
