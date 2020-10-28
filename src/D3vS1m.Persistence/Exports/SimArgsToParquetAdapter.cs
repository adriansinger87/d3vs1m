using D3vS1m.Application.Network;
using D3vS1m.Domain.Data.Arguments;
using Parquet.Data;
using Sin.Net.Domain.Persistence.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Persistence.Exports
{
    public class SimArgsToParquetAdapter : AdapterBase
    {

        // -- fields

        private List<DataColumn> _columns;

        // -- methods

        public override Tout Adapt<Tin, Tout>(Tin input)
        {
            // validation
            if (!ValidateTypes<List<ArgumentsBase>, List<DataColumn>>(typeof(Tin), typeof(Tout)))
            {
                return new Tout();
            }

            var args = input as List<ArgumentsBase>;

            _columns = new List<DataColumn>();

            foreach(var arg in args)
            {
                if (arg is NetworkArgs) Adapt(arg as NetworkArgs);
            }

            return ConvertOutput<Tout>(_columns);
        }

        private void Adapt(NetworkArgs arg)
        {
            arg.Network.RssMatrix.Each((r, c, val) =>
            {
                var name = $"rss-{r}-{c}";
                _columns.Add(NewColumn<float>(name, new float[] { val }));
                return val;
            });
        }

        private DataColumn NewColumn<T>(string name, T[] data)
        {
            return new DataColumn(
               new DataField<T>(name), data);
        }
    }
}
