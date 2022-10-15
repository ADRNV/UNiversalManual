using MediatR;
using UMan.Core;
using UMan.Core.Repositories;

namespace UMan.Domain.Papers
{
    public class Delete
    {
        public record Command(int id) : IRequest<bool>;

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IRepository<Paper> _papersRepository;

            public Handler(IRepository<Paper> papersRepository)
            {
                _papersRepository = papersRepository;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _papersRepository.Delete(request.id, cancellationToken);
            }
        }
    }
}
