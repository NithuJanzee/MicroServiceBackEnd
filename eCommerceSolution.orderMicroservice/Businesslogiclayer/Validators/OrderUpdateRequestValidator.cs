using eCommerce.OrderMicroservice.BusinessLogicLayer.DTO;
using FluentValidation;

namespace eCommerce.OrderMicroservice.Businesslogiclayerr.Validators;

public class OrderUpdateRequestValidator:AbstractValidator<OrderUpdateRequest>
{
    public OrderUpdateRequestValidator()
    {
        //order id
        RuleFor(id => id.OrderID).NotEmpty().WithMessage("OrderId is required");
        //user id
        RuleFor(temp => temp.UserID).NotEmpty().WithMessage("UserId is required");
        //order date
        RuleFor(date => date.OrderDate).NotEmpty().WithMessage("OrderDate is required");
        //order items
        RuleFor(orderItems => orderItems.OrderItems).NotEmpty().WithMessage("OrderItems is required");
    }

}
