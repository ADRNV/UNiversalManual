using MediatR;
using UMan.Core;
using UMan.Core.Pagination;
using UMan.Core.Repositories;

namespace UMan.Domain.Papers
{
    public class Get
    {
        public record CommandByQueryParameters(QueryParameters QueryParameters) : IRequest<Page<Paper>>;

        public class HandlerByQueryParameters : IRequestHandler<CommandByQueryParameters, Page<Paper>>
        {
            private readonly IRepository<Paper> _papersRepository;

            public HandlerByQueryParameters(IRepository<Paper> papersRepository)
            {
                _papersRepository = papersRepository;
            }

            public async Task<Page<Paper>> Handle(CommandByQueryParameters request, CancellationToken cancellationToken)
            {
                return await _papersRepository.Get(request.QueryParameters, cancellationToken);
            }
        }

        public record CommandById(int Id) : IRequest<Paper>;

        public class HandlerById : IRequestHandler<CommandById, Paper>
        {
            private readonly IRepository<Paper> _papersRepository;

            public HandlerById(IRepository<Paper> papersRepository)
            {
                _papersRepository = papersRepository;
            }

            public async Task<Paper> Handle(CommandById request, CancellationToken cancellationToken)
            {
                return await _papersRepository.Get(request.Id, cancellationToken);
            }
        }

        public record CommandByTags(IEnumerable<HashTag> HashTags) : IRequest<IEnumerable<Paper>>;

        public class Handler : IRequestHandler<CommandByTags, IEnumerable<Paper>>
        {
            private readonly IPapersRepository _papersRepository;

            public Handler(IPapersRepository papersRepository)
            {
                _papersRepository = papersRepository;
            }

            public async Task<IEnumerable<Paper>> Handle(CommandByTags request, CancellationToken cancellationToken)
            {
                return await _papersRepository.GetByTag(request.HashTags);
            }
        }

        //TODO Add validation
    }
}
