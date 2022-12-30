using MediatR;
using System.Net;
using UMan.API.ApiModels;
using UMan.Core;
using UMan.Core.Repositories;

namespace UMan.Domain.Papers
{
    public class Delete
    {
        public record Command(int Id) : IRequest<bool>;

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Paper> _papersRepository;

            public Handler(IRepository<Paper> papersRepository)
            {
                _papersRepository = papersRepository;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken) =>
                await _papersRepository.Delete(request.Id, cancellationToken) ? true : throw new RestException(HttpStatusCode.NotFound);
        }
    }
}
