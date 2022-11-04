using FluentValidation;
using MediatR;
using UMan.Core;
using UMan.Core.Repositories;
using UMan.Domain.Papers.Validation;

namespace UMan.API.Features.Authors
{
    public class Create
    {
        public record Command(Author Author) : IRequest<int>;

        public class Handler : IRequestHandler<Command, int>
        {
            IRepository<Author> _repository;

            public Handler(IRepository<Author> repository)
            {
                _repository = repository;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _repository.Add(request.Author, cancellationToken);
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(c => c.Author).NotNull().SetValidator(new AuthorValidation());
                }
            }
        }
    }
}
