using D3vS1m.Application;
using System;
using System.Collections.Generic;

namespace D3vS1m.Cli
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Log.I
            Console.WriteLine($"Starting D3vS1m command line tool with the args: {string.Join(", ", args)}.");

            ReadArgs(args);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static void ReadArgs(string[] args)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();

            for (int i = 0; i < args.Length; i += 2)
            {

                values.Add(feedKey(args[i]), args[i + 1]);

            }

            string feedKey(string arg)
            {
                var a = arg.ToLower();
                if (a == "-n" || a == "network")
                {
                    return Models.Network.Key;
                }

                if (a == "-a" || a == "antenna")
                {
                    return Models.Network.Key;
                }

                if (a == "-d" || a == "devices")
                {
                    return Models.Network.Key;
                }

                if (a == "-r" || a == "radio")
                {
                    return Models.Channel.AdaptedFriis.Key;
                }

                return "";
            }
        }
    }
}