using MediatR;
using UMan.Core;
using UMan.Core.Repositories;

namespace UMan.Domain.Papers
{
    public record Command(int id) : IRequest<Paper>;

    public class Handler : IRequestHandler<Command, Paper>
    {
        private readonly IRepository<Paper> _papersRepository;

        public Handler(IRepository<Paper> papersRepository)
        {
            _papersRepository = papersRepository;
        }

        public async Task<Paper> Handle(Command request, CancellationToken cancellationToken)
        {
            return await _papersRepository.Get(request.id);
        }
    }
}
