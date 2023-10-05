using FeedParser.Parsers.Updates.Schedulers;
using MediatR;
using UMan.API.ApiModels;
using UMan.Core.Services;
using UMan.Services.Parsing;

namespace UMan.API.Features.Papers
{
    public class ActionParsing
    {
        public record Command(string Action) : IRequest<bool>;

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IParsingServiceDispatcher<UpdateScheduler> _parsingServiceDispatcher;

            public Handler(IParsingServiceDispatcher<UpdateScheduler> parserServiceDispatcher)
            {
                _parsingServiceDispatcher = parserServiceDispatcher;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                switch (request.Action)
                {
                    case "start":
                        var cancel = new CancellationTokenSource(TimeSpan.FromSeconds(3));

                        await _parsingServiceDispatcher.StopAsync(cancellationToken);
                    break;

                    case "stop":
                        await _parsingServiceDispatcher.StopAsync(cancellationToken);
                    break;

                    default:
                        throw new RestException(System.Net.HttpStatusCode.NotFound);
                }
               
                return await Task.FromResult(true);
            }
        }
    }
}
