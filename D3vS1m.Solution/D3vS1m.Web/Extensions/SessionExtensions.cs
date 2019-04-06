using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace D3vS1m.Web.Extensions
{
    public static class SessionExtensions
    {
        //TODO: remove dependency to newtonsoft and use io -> json
        //private static IPersistenceControlable _io = new PersistenceController();

        public static void SetData(this ISession session, string key, object data)
        {
            session.SetString(key, JsonConvert.SerializeObject(data));
        }

        public static T GetData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            return data == null ? default(T) : JsonConvert.DeserializeObject<T>(data);
        }

    }
}
