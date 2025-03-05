using eCommerce.OrderMicroservice.BusinessLogicLayer.DTO;
using FluentValidation;

namespace eCommerce.OrderMicroservice.Businesslogiclayer.Validators;

public class OrderItemAddRequestValidator : AbstractValidator<OrderItemAddRequest>
{
    public OrderItemAddRequestValidator()
    {
        //product id
        RuleFor(id => id.ProductId).NotEmpty().WithMessage("ProductId is required");
        //unit price
        RuleFor(price => price.UnitPrice).NotEmpty().WithMessage("UnitPrice is required");
        //quantity
        RuleFor(quantity => quantity.Quantity).NotEmpty().WithMessage("Quantity is required");
    }
}
