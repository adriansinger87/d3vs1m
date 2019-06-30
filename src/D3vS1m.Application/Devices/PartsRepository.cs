using Sin.Net.Domain.Repository;
using System;

namespace D3vS1m.Application.Devices
{
    [Serializable]
    public class PartsRepository : RepositoryBase<object>
    {
        public PartsRepository()
        {
            Name = "part repository";
        }
    }
}
