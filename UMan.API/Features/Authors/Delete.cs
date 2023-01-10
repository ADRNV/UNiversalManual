using MediatR;
using System.Net;
using UMan.API.ApiModels;
using UMan.Core;
using UMan.Core.Repositories;

namespace UMan.API.Features.Authors
{
    public class Delete
    {
        public record Command(int Id) : IRequest<bool>;

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Author> _repository;

            public Handler(IRepository<Author> repository)
            {
                _repository = repository;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken) =>
                await _repository.Delete(request.Id, cancellationToken) ? true : throw new RestException(HttpStatusCode.NotFound);

        }
    }
}
