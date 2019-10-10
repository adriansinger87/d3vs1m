using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Sin.Net.Domain.Persistence.Logging;

namespace D3vS1m.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Inject(new Sin.Net.Logging.NLogger());

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
