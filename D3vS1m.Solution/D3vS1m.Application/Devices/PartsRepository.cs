using Sin.Net.Domain.Repository;

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
