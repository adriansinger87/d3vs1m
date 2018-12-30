using D3vS1m.Domain.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Devices
{
    // TODO define the type that represents parts of an device
    public class PartsRepository : RepositoryBase<object>
    {
        public PartsRepository()
        {
            Name = "part repository";
        }
    }
}
