using D3vS1m.Application.Antenna;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Communication;
using D3vS1m.Application.Energy;
using D3vS1m.Application.Network;
using D3vS1m.Application.Scene;
using D3vS1m.Domain.Data.Arguments;
using Microsoft.AspNetCore.Http;
using Sin.Net.Persistence.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Sin.Net.Persistence.IO.Json;

namespace D3vS1m.WebAPI.Extensions
{
    // TODO: Issue! long arrays are stored as json string in the session that makes the load and save procedure very long
    // - #1 try to store the string as compressed binary code
    // - #2 safe long data array on disk and never in session
    public static class HttpSessionExtensions
    {
        public static readonly string ARGUMENTS_KEY = "ARGUMENTS_KEY";

        private static TypedSerializationBinder _binder;

        /// <summary>
        /// Gets the information if the key is already used in the session.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Contains(this ISession session, string key)
        {
            return session.Keys.Contains(key);
        }

        /// <summary>
        /// Loads the generic value from the session that matches to the given key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) :
                JsonIO.FromJsonString<T>(value);
        }

        /// <summary>
        /// Stores the generic value into the session with the given key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set<T>(this ISession session, string key, T value)
        {
            var json = JsonIO.ToJsonString(value);
            session.SetString(key, json);
        }

        /// <summary>
        /// Loads the generic ArgumentsBase array from the session with a predefined key.
        /// </summary>
        /// <param name="session">The calling session isntance.</param>
        public static ArgumentsBase[] GetArguments(this ISession session)
        {
            var value = session.GetString(ARGUMENTS_KEY);

            return value == null ? default(ArgumentsBase[]) :
                JsonIO.FromJsonString<ArgumentsBase[]>(value, ArgumentsBinder);
        }

        /// <summary>
        /// Stores the generic ArgumentsBase array into the session with a predefined key.
        /// </summary>
        /// <param name="session">The calling session instance.</param>
        /// <param name="args">The new array to save.</param>
        public static void SetArguments(this ISession session, ArgumentsBase[] args)
        {
            var json = JsonIO.ToJsonString(args, ArgumentsBinder);
            session.SetString(ARGUMENTS_KEY, json);
        }



        //TODO: should be checked again
        /// <summary>
        /// Gets the binder for the concrete types for json-serialization.
        /// </summary>
        public static TypedSerializationBinder ArgumentsBinder
        {
            get
            {
                if (_binder == null)
                {
                    _binder = new TypedSerializationBinder(new List<Type> {
                        typeof(AdaptedFriisArgs),
                        typeof(SimpleAntennaArgs),
                        typeof(FlatAntennaArgs),
                        typeof(SphericAntennaArgs),
                        typeof(WirelessCommArgs),
                        typeof(BatteryArgs),
                        typeof(NetworkArgs),
                        typeof(InvariantSceneArgs)
                    });
                }
                return _binder;
            }
        }
    }
}
