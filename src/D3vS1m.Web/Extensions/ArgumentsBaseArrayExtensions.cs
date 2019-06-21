using D3vS1m.Domain.Data.Arguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D3vS1m.Web.Extensions
{
    public static class ArgumentsBaseArrayExtensions
    {

        // -- Name

        public static ArgumentsBase GetByName(this ArgumentsBase[] args, string name)
        {
            return args.FirstOrDefault(a => a.Name == name);
        }

        public static void SetByName(this ArgumentsBase[] args, string name, ArgumentsBase value)
        {
            var index = args.GetIndexByName(name);
            args[index] = value;
        }

        public static int GetIndexByName(this ArgumentsBase[] args, string name)
        {
            return args.ToList().IndexOf(args.GetByName(name));
        }

        // -- Guid

        public static ArgumentsBase GetByGuid(this ArgumentsBase[] args, string guid)
        {
            return args.FirstOrDefault(a => a.Guid == guid);
        }

        public static void SetByGuid(this ArgumentsBase[] args, string guid, ArgumentsBase value)
        {
            var index = args.GetIndexByGuid(guid);
            args[index] = value;
        }

        public static int GetIndexByGuid(this ArgumentsBase[] args, string guid)
        {
            return args.ToList().IndexOf(args.GetByGuid(guid));
        }
    }
}
