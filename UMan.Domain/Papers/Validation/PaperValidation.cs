using FluentValidation;
using UMan.Core;

namespace UMan.Domain.Papers.Validation
{
    public class PaperValidation : AbstractValidator<Paper>
    {
        public PaperValidation()
        {
            RuleFor(p => p.Id).Must(id => id == 0);
            RuleFor(p => p.Author).NotNull();
            RuleFor(p => p.Created).NotNull();
        }
    }
}
