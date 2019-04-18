using Sin.Net.Domain.Repository;
using System;

namespace D3vS1m.Application.Devices
{
    // TODO define the type that represents parts of an device
    [Serializable]
    public class PartsRepository : RepositoryBase<object>
    {
        public PartsRepository()
        {
            Name = "part repository";
        }
    }
}
