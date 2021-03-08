using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.WebApi.GraphQL.Types;
using GraphQL.Types;

namespace D3vS1m.WebApi.GraphQL
{
	public class GraphQLQuery : ObjectGraphType
	{
		// -- fields

		private IEnumerable<ArgumentsBase> _args;

		// -- constructor

		public GraphQLQuery()
		{
			Field<ListGraphType<ArgumentsType>>("arguments", resolve: context => _args);
		}

		// -- methods

		void InitArguments(IEnumerable<ArgumentsBase> args)
		{
			_args = args;
		}
	}
}
