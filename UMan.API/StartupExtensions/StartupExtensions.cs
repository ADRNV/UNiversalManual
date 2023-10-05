using FeedParser.Parsers.Updates.Schedulers;
using Microsoft.Extensions.Hosting;
using FeedParser.Extensions;

namespace UMan.API.StartupExtensions
{
    public static class StartupExtensions
    {
        public static IHostBuilder ConfigureParserService(this IHostBuilder builder)
        {
            builder.ConfigureParsers();

            builder.ConfigureScheduler();

            return builder;
        }
    }
}
