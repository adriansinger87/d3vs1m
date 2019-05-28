using D3vS1m.Application.Antenna;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Communication;
using D3vS1m.Application.Energy;
using D3vS1m.Application.Network;
using D3vS1m.Application.Scene;
using D3vS1m.Web.System.Enumerations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sin.Net.Persistence.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace D3vS1m.Web.Extensions
{
    public static class SessionExtensions
    {

        public static void SetData(this ISession session, string key, object data, SessionStorages storage = SessionStorages.Binary)
        {
            switch (storage)
            {
                case SessionStorages.Json:
                    SetJson(session, key, data);
                    break;
                case SessionStorages.Binary:
                    SetBinary(session, key, data);
                    break;
                default:
                    SetJson(session, key, data);
                    break;
            }
        }

        public static T GetData<T>(this ISession session, string key, SessionStorages storage = SessionStorages.Binary)
        {
            switch (storage)
            {
                case SessionStorages.Json:
                    return GetJson<T>(session, key);
                case SessionStorages.Binary:
                    return GetBinary<T>(session, key);
                default:
                    return GetJson<T>(session, key);
            }
        }

        // -- json

        private static void SetJson(ISession session, string key, object data)
        {
            session.SetString(key, JsonIO.ToJsonString(data, ContextBinder));
        }

        private static T GetJson<T>(ISession session, string key)
        {
            var json = session.GetString(key);
            return (json ==null ? default(T) : JsonIO.FromJsonString<T>(json, ContextBinder));
        }

        // -- binary

        private static void SetBinary(this ISession session, string key, object data)
        {
            byte[] bytes = BinaryIO.ToBytes(data);
            session.Set(key, bytes);
        }

        private static T GetBinary<T>(ISession session, string key)
        {
            T data = default(T);
            var bytes = session.Get(key);

            data = BinaryIO.FromBytes<T>(bytes);

            return data;
        }

        private static ISerializationBinder _binder;

        private static ISerializationBinder ContextBinder
        {
            get
            {
                if (_binder == null)
                {
                    _binder = new TypedSerializationBinder(new List<Type> {
                        typeof(AdaptedFriisArgs),
                        typeof(AdaptedFriisSimulator),
                        typeof(SimpleAntennaArgs),
                        typeof(SimpleAntennaSimulator),
                        typeof(SphericAntennaArgs),
                        typeof(SphericAntennaSimulator),
                        typeof(WirelessCommArgs),
                        typeof(LRWPANSimulator),
                        typeof(BatteryArgs),
                        typeof(BatteryPackSimulator),
                        typeof(NetworkArgs),
                        typeof(PeerToPeerNetworkSimulator),
                        typeof(InvariantSceneArgs),
                        typeof(SceneSimulator)
                    });
                }
                return _binder;
            }
        }
    }
}
