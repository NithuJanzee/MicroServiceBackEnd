using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;
using FluentValidation;

namespace BusinessLogicLayer.Validators
{
    public class ProductUpdateRequestValidators : AbstractValidator<ProductUpdateRequest>
    {
        public ProductUpdateRequestValidators()
        {
            RuleFor(id => id.ProductID)
                .NotEmpty().WithMessage("Product ID is not valid");
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
