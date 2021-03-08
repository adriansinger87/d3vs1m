using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D3vS1m.Domain.Data.Arguments;
using GraphQL.Types;

namespace D3vS1m.WebApi.GraphQL.Types
{
	public class ArgumentsType : ObjectGraphType<ArgumentsBase>
	{
		public ArgumentsType()
		{
			Field(x => x.Key, type: typeof(IdGraphType)).Description("The key identifier of the simulation model.");
			Field(x => x.Name).Description("The name of the simulation model.");
			Field(x => x.Index).Description("The index indicates the order of the executed simulation models.");
		}
	}
}
