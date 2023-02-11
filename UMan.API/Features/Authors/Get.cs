using MediatR;
using System.Net;
using UMan.API.ApiModels;
using UMan.Core;
using UMan.Core.Pagination;
using UMan.Core.Repositories;

namespace UMan.API.Features.Authors
{
    public class Get
    {
        public record CommandByQueryParameters(QueryParameters QueryParameters) : IRequest<Page<Author>>;

        public class HandlerByQueryParameters : IRequestHandler<CommandByQueryParameters, Page<Author>>
        {
            private readonly IRepository<Author> _repository;

            public HandlerByQueryParameters(IRepository<Author> repository)
            {
                _repository = repository;
            }

            public async Task<Page<Author>> Handle(CommandByQueryParameters request, CancellationToken cancellationToken)
            {
                return await _repository.Get(request.QueryParameters, cancellationToken);
            }
        }

        public record CommandById(int Id) : IRequest<Author>;

        public class HandlerById : IRequestHandler<CommandById, Author>
        {
            private readonly IRepository<Author> _repository;

            public HandlerById(IRepository<Author> repository)
            {
                _repository = repository;
            }

            public async Task<Author> Handle(CommandById request, CancellationToken cancellationToken) =>
                await _repository.Get(request.Id) ?? throw new RestException(HttpStatusCode.NotFound);
        }
    }
}
