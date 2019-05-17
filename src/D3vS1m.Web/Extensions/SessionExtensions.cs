using D3vS1m.Persistence;
using D3vS1m.Web.System.Enumerations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace D3vS1m.Web.Extensions
{
    public static class SessionExtensions
    {

        public static void SetData(this ISession session, string key, object data, SessionStorages storage = SessionStorages.Json)
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

        public static T GetData<T>(this ISession session, string key, SessionStorages storage = SessionStorages.Json)
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
            // TODO implement json binary with TypedSerializationBinder

            session.SetString(key, JsonConvert.SerializeObject(data));
        }

        private static T GetJson<T>(ISession session, string key)
        {
            var data = session.GetString(key);
            return data == null ? default(T) : JsonConvert.DeserializeObject<T>(data);
        }

        // -- binary

        private static void SetBinary(this ISession session, string key, object data)
        {
            byte[] bytes;
            IFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                using (var ds = new DeflateStream(stream, CompressionMode.Compress, true))
                {
                    formatter.Serialize(ds, data);
                }
                bytes = stream.ToArray();
            }

            session.Set(key, bytes);
        }

        private static T GetBinary<T>(ISession session, string key)
        {
            T data = default(T);
            var bytes = session.Get(key);

            IFormatter formatter = new BinaryFormatter();
            using (var stream = new MemoryStream(bytes))
            {
                using (var ds = new DeflateStream(stream, CompressionMode.Decompress, true))
                {
                    data = (T)formatter.Deserialize(ds);
                }
            }

            return data;
        }
    }
}
