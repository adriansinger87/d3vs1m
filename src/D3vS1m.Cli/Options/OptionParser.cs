using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace D3vS1m.Cli.Options
{
    public class OptionParser
    {
        // --fields
        private const string PREFIX = "-";

        private CliOptions _options;

        // -- constructor

        public OptionParser()
        {
            _options = new CliOptions();
        }

        // -- methods

        public CliOptions ReadArguments(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];

                if (arg.StartsWith(PREFIX) == false)
                {
                    continue;
                }

                SetOption(arg, i, args);
            }
            return _options;
        }

        private void SetOption(string arg, int index, string[] args)
        {
            arg = Regex.Replace(arg, @"[^0-9a-zA-Z]+", "");

            foreach (var prop in _options.GetType().GetProperties())
            {
                var attr = prop.GetCustomAttributes<CliAttribute>().First();

                if (!attr.Short.Equals(arg) && !attr.Long.Equals(arg))
                {
                    continue;
                }

                if (prop.PropertyType == typeof(bool))
                {
                    prop.SetValue(_options, true);
                }
                else
                {
                    var val = args.Length > index + 1 ? args[index + 1] : string.Empty;
                    prop.SetValue(_options, val);
                }
            }
        }
    }
}
