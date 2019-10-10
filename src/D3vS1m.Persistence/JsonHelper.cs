using D3vS1m.Application.Scene.Materials;
using Newtonsoft.Json.Serialization;
using Sin.Net.Persistence.IO.Json;
using System;
using System.Collections.Generic;

namespace D3vS1m.Persistence
{
    public static class JsonHelper
    {
        public static ISerializationBinder Binder
        {
            get
            {
                return new TypedSerializationBinder(new List<Type> {
                    typeof(MaterialPhysics),
                });
            }
        }
    }
}
