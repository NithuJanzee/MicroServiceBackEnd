using eCommerce.OrderMicroservice.BusinessLogicLayer.DTO;
using FluentValidation;

namespace eCommerce.OrderMicroservice.Businesslogiclayer.Validators;

public class OrderAddRequestValidators:AbstractValidator<OrderAddRequest>
{
    public OrderAddRequestValidators()
    {
        //user id
        RuleFor(temp => temp.UserId).NotEmpty().WithMessage("UserId is required");
        //order date
        RuleFor(date => date.OrderDate).NotEmpty().WithMessage("OrderDate is required");
        //order items
        RuleFor(orderItems => orderItems.OrderItems).NotEmpty().WithMessage("OrderItems is required");
    }
}
