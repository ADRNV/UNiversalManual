using FluentValidation;
using UMan.Core;

namespace UMan.Domain.Papers.Validation
{
    public class AuthorValidation : AbstractValidator<Author>
    {
        public AuthorValidation()
        {
            RuleFor(a => a.Email).EmailAddress();
            RuleFor(a => a.Name).NotNull().NotEmpty();
        }
    }
}
