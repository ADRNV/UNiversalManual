using MediatR;
using UMan.Core;
using UMan.Core.Repositories;

namespace UMan.Domain.Papers
{
    public class Update
    {
        public record Command(Paper Paper, int id) : IRequest<int>;

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IRepository<Paper> _papersRepository;

            public Handler(IRepository<Paper> papersRepository)
            {
                _papersRepository = papersRepository;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _papersRepository.Update(request.id, request.Paper, cancellationToken);
            }
        }
    }
}
