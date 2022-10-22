using FluentValidation;
using MediatR;
using UMan.Core;
using UMan.Core.Repositories;
using UMan.Domain.Papers.Validation;

namespace UMan.Domain.Papers
{
    public class Create
    {
        public record Command(Paper Paper) : IRequest<int>;

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly IRepository<Paper> _papersRepository;

            public Handler(IRepository<Paper> papersRepository)
            {
                _papersRepository = papersRepository;
            }

            public Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                return _papersRepository.Add(request.Paper, cancellationToken);
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.Paper).NotNull().SetValidator(new PaperValidation());
            }
        }



    }
}
