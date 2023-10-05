using UMan.API;
using UMan.API.StartupExtensions;

#pragma warning disable CS1591
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args)
            .ConfigureParserService()
            .Build()
            .Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}