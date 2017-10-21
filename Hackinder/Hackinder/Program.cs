using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Hackinder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://*:80")
#if !DEBUG
                .UseKestrel(options =>
                {   
                    options.Listen(IPAddress.Parse("77.244.217.178", 80);
                    options.Listen(IPAddress.Parse("77.244.217.178"), 443, listenOptions =>
                    {
                        listenOptions.UseHttps("hackinder.pfx", "ololo");
                    });
                })
#endif
                .Build();
    }
}
