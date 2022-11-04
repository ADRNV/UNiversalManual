using MediatR;
using UMan.Core;
using UMan.Core.Repositories;

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
                return await _repository.Update(request.Id, request.Author, cancellationToken);
            }
        }
    }
}
