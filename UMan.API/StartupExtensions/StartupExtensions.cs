using FeedParser.Parsers.Updates.Schedulers;
using Microsoft.Extensions.Hosting;
using FeedParser.Extensions;
using FeedParser.Core;
using FeedParser.Core.Models;
using UMan.DataAccess.Parsing.Handlers;
using UMan.Core.Repositories;
using FeedParser.Parsers.Updates.Schedulers.Configuration;
using UMan.DataAccess.Repositories;
using MediatR;
using System.Reflection;
using UMan.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace UMan.API.StartupExtensions
{
    public static class StartupExtensions
    {
        public static IHostBuilder ConfigureParserService(this IHostBuilder builder)
        {
            builder.ConfigureParsers();

            builder.ConfigureServices(services =>
            {
                services.AddSingleton(s => new SchedulerOptions { UpdatesInterval = TimeSpan.FromSeconds(3) });

                services.AddTransient<IUpdateHandler<IEnumerable<Article>>, UpdateHandler>(sp =>
                {
                    var scope = sp.CreateScope();
                    
                    var repository = scope.ServiceProvider.GetRequiredService<IPapersRepository>();
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<UpdateHandler>>();
                    return new UpdateHandler(repository, logger);
                });
               

                services.AddHostedService<UpdateScheduler>();
            });

            return builder;
        }
    }
}
