using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D3vS1m.Persistence;
using D3vS1m.Domain.IO;
using D3vS1m.Domain.System.Enumerations;
using Newtonsoft.Json;

namespace D3vS1m.Web.Extensions
{
    public static class SessionExtensions
    {
        //TODO: remove dependency to newtonsoft and use io -> json
        private static IOControllable _io = new IOController();

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
