using FluentValidation;
using UMan.Core;

namespace UMan.API.Features.Validation
{
    public class HashTagValidation : AbstractValidator<HashTag>
    {
        public HashTagValidation()
        {
            RuleFor(h => h.Title)
                .NotNull()
                .NotEmpty()
                .Must(t => t.StartsWith('#'));
        }
    }
}
