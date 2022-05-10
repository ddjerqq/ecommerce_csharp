using ecommerce.Core.Models;
using FluentValidation;

namespace ecommerce.Core.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Username)
                .NotEmpty()
                .WithMessage("{PropertyName} should not be empty")
                .Length(3, 32)
                .WithMessage("{PropertyName} should be from 3 to 32 characters");
            
            RuleFor(u => u.Experience)
                .GreaterThanOrEqualTo((uint) 0)
                .WithMessage("{PropertyName} should be greater than or equal to 0");
            
            RuleFor(u => u.Bank)
                .GreaterThanOrEqualTo((uint) 0)
                .WithMessage("{PropertyName} should be greater than or equal to 0");
            
            RuleFor(u => u.Wallet)
                .GreaterThanOrEqualTo((uint) 0)
                .WithMessage("{PropertyName} should be greater than or equal to 0");
        }
    }
}