using D3vS1m.Domain.System.Logging;
using D3vS1m.Persistence.Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace D3vS1m.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Inject(new NLogger());

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
