using ecommerce.Core.Models;
using FluentValidation;

namespace ecommerce.Core.Validators
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(x => x.Username)
                .Matches("^[a-zA-Z0-9._]*$")
                .WithMessage("Username can only contain letters, numbers, dots and underscores");
        }
    }
}