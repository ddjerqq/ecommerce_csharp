using ecommerce.Core.Models;
using ecommerce.Core.Models.DataTransferObjects;
using FluentValidation;

namespace ecommerce.Core.Validators
{
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} should not be empty")
                .Length(3, 32)
                .WithMessage("{PropertyName} should be from 3 to 32 characters");

            RuleFor(e => e.Description)
                .NotEmpty()
                .WithMessage("{PropertyName} should not be empty")
                .Length(3, 128)
                .WithMessage("{PropertyName} should be from 3 to 128 characters");
            
            RuleFor(e => e.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} should be greater than or equal to 0");

            RuleFor(e => e.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("{PropertyName} should be greater than or equal to 0");
        }
    }
}