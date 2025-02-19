using BusinessLogicLayer.DTO;
using FluentValidation;

namespace BusinessLogicLayer.Validators
{
    public class ProductAddRequestValidators : AbstractValidator<ProductAddRequest>
    {
        public ProductAddRequestValidators()
        {
            RuleFor(RuleFor => RuleFor.ProductName)
                .NotEmpty().WithMessage("Product Name is Can't be empty");
            RuleFor(c => c.Category)
                .IsInEnum().WithMessage("Category is not valid");
            RuleFor(d => d.UnitPrice)
                .InclusiveBetween(0, double.MaxValue).WithMessage($"Unit Price should between 0 to {double.MaxValue}");
            RuleFor(q => q.QuantityInStock)
                .InclusiveBetween(0, int.MaxValue).WithMessage($"Quantity in stock should between 0 to {int.MaxValue}");
        }
    }
}
