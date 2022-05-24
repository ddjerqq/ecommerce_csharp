using ecommerce.Core.Models;
using ecommerce.Core.Models.DataTransferObjects;
using FluentValidation;

namespace ecommerce.Core.Validators
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(e => e.Quantity)
                .GreaterThanOrEqualTo(1)
                .WithMessage("{PropertyName} must be greater than 0");

            RuleFor(e => e.ProductId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required");

            RuleFor(e => e.CustomerId)
                .NotEmpty()
                .WithMessage("{PropertyName} is required");
        }
    }
}