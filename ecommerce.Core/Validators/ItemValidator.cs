using ecommerce.Core.Models;
using FluentValidation;

namespace ecommerce.Core.Validators
{
    public class ItemValidator : AbstractValidator<Item>
    {
        public ItemValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
            
            RuleFor(x => x.OwnerId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
        }
    }
}