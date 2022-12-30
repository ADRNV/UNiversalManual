using MediatR;
using System.Net;
using UMan.API.ApiModels;
using UMan.Core;
using UMan.Core.Repositories;
using UMan.DataAccess.Repositories.Exceptions;

namespace UMan.Domain.Papers
{
    public class Update
    {
        public record Command(Paper Paper, int Id) : IRequest<int>;

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IRepository<Paper> _papersRepository;

            public Handler(IRepository<Paper> papersRepository)
            {
                _papersRepository = papersRepository;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    return await _papersRepository.Update(request.Id, request.Paper, cancellationToken);
                }
                catch (EntityNotFoundException<int>)
                {
                    throw new RestException(HttpStatusCode.NotFound);
                }
            }
        }
    }
}
