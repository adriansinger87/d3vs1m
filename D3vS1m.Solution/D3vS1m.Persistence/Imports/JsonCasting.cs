using D3vS1m.Domain.IO;
using Newtonsoft.Json;
using Sin.Net.Domain.Logging;
using System;

namespace D3vS1m.Persistence.Imports
{
    public class JsonCasting : ICasteable
    {
        public Tout CastTo<Tout, Tin>(Tin input) where Tout : new()
        {
            Tout obj = new Tout();
            try
            {
                string json = input as string;
                obj = JsonConvert.DeserializeObject<Tout>(json);
            }
            catch (Exception ex)
            {
                Log.Error($"Could not cast from '{typeof(Tin).Name}' to '{typeof(Tout).Name}' ");
                Log.Error(ex.Message);
            }

            return obj;
        }
    }
}
