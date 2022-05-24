using ecommerce.Core.Models;
using FluentValidation;

namespace ecommerce.Core.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(e => e.Quantity)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Quantity must be greater than 0");
        }
    }
}