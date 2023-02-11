using MediatR;
using System.Net;
using UMan.API.ApiModels;
using UMan.Core;
using UMan.Core.Repositories;
using UMan.DataAccess.Repositories.Exceptions;

namespace UMan.API.Features.Authors
{
    public class Update
    {
        public record Command(Author Author, int Id) : IRequest<int>;

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IRepository<Author> _repository;

            public Handler(IRepository<Author> repository)
            {
                _repository = repository;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    return await _repository.Update(request.Id, request.Author, cancellationToken);
                }
                catch (EntityNotFoundException<int>)
                {
                    throw new RestException(HttpStatusCode.NotFound);
                }
            }
        }
    }
}
