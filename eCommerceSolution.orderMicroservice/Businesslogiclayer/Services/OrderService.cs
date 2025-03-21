using System.Linq.Expressions;
using AutoMapper;
using eCommerce.OrderMicroservice.Businesslogiclayer.HttpClients;
using eCommerce.OrderMicroservice.Businesslogiclayer.ServiceContact;
using eCommerce.OrderMicroservice.BusinessLogicLayer.DTO;
using eCommerce.OrderMicroservice.DataAccessLayer.Entity;
using eCommerce.OrderMicroservice.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace eCommerce.OrderMicroservice.Businesslogiclayer.Services;

public class OrderService : IorderService
{
    private readonly IValidator<OrderAddRequest> _OrderAddrequestValidator;
    private readonly IValidator<OrderItemAddRequest> _OrderItemAddrequestValidator;
    private readonly IValidator<OrderItemUpdateRequest> _OrderItemUpdaterequestValidator;
    private readonly IValidator<OrderUpdateRequest> _OrderUpdaterequestValidator;
    private readonly IorderRepository _iorderRepository;
    private readonly IMapper _mapper;
    private readonly UsersMicroServiceClint _usersMicroServiceClinet;

    public OrderService(
        IValidator<OrderAddRequest> orderAddrequestValidator,
        IValidator<OrderItemAddRequest> orderItemAddrequestValidator,
        IValidator<OrderItemUpdateRequest> orderItemUpdaterequestValidator,
        IValidator<OrderUpdateRequest> orderUpdaterequestValidator,
        IorderRepository iorderRepository,
        IMapper mapper,
        UsersMicroServiceClint usersMicroService)
    {
        _OrderAddrequestValidator = orderAddrequestValidator;
        _OrderItemAddrequestValidator = orderItemAddrequestValidator;
        _OrderItemUpdaterequestValidator = orderItemUpdaterequestValidator;
        _OrderUpdaterequestValidator = orderUpdaterequestValidator;
        _iorderRepository = iorderRepository;
        _mapper = mapper;
        _usersMicroServiceClinet = usersMicroService;
    }

    public async Task<OrderResponse> AddOrder(OrderAddRequest orderAddRequest)
    {
        if (orderAddRequest == null)
        {
            throw new ArgumentNullException(nameof(orderAddRequest));
        }

        // Validate using fluent validation
        ValidationResult OrderAddvalidationResult = await _OrderAddrequestValidator.ValidateAsync(orderAddRequest);
        if (!OrderAddvalidationResult.IsValid)
        {
            string error = string.Join(",", OrderAddvalidationResult.Errors.Select(temp => temp.ErrorMessage));
            throw new ArgumentException(error);
        }

        // Validate order items
        foreach (OrderItemAddRequest orderItemAddRequest in orderAddRequest.OrderItems)
        {
            ValidationResult validationResult = await _OrderItemAddrequestValidator.ValidateAsync(orderItemAddRequest);
            if (!validationResult.IsValid)
            {
                string errors = string.Join(",", validationResult.Errors.Select(temp => temp.ErrorMessage));
                throw new ArgumentException(errors);
            }
        }

        // Check if user exists
        UserDTO? user = await _usersMicroServiceClinet.GetUserByUserId(orderAddRequest.UserId);
        if (user == null)
        {
            throw new ArgumentException("Invalid user Id");
        }

        Order orderInput = _mapper.Map<Order>(orderAddRequest);

        // Calculate total values
        foreach (OrderItem orderItem in orderInput.OrderItems)
        {
            orderItem.TotalPrice = orderItem.Quantity * orderItem.UnitPrice;
        }

        orderInput.TotalBill = orderInput.OrderItems.Sum(temp => temp.TotalPrice);

        // Add order
        Order addedOrder = await _iorderRepository.AddOrder(orderInput);

        if (addedOrder == null)
        {
            return null;
        }

        // Map to response
        OrderResponse addedOrderResponse = _mapper.Map<OrderResponse>(addedOrder);
        return addedOrderResponse;
    }

    public async Task<bool> DeleteOrder(Guid orderID)
    {
        return await _iorderRepository.DeleteOrder(orderID);
    }

    public async Task<OrderResponse> GetOrderByCondition(Expression<Func<Order, bool>> predicate)
    {
        Order order = await _iorderRepository.GetOrderByCondition(predicate);
        if (order == null)
        {
            return null;
        }

        OrderResponse response = _mapper.Map<OrderResponse>(order);
        return response;
    }

    public async Task<List<OrderResponse>> GetOrders()
    {
        IEnumerable<Order> orders = await _iorderRepository.GetOrders();
        IEnumerable<OrderResponse> orderResponses = _mapper.Map<IEnumerable<OrderResponse>>(orders);
        return orderResponses.ToList();
    }

    public async Task<List<OrderResponse>> GetOrdersByCondition(Expression<Func<Order, bool>> predicate)
    {
        IEnumerable<Order> orders = await _iorderRepository.GetOrdersByCondition(predicate);
        if (orders == null)
        {
            return null;
        }

        IEnumerable<OrderResponse> responses = _mapper.Map<IEnumerable<OrderResponse>>(orders);
        return responses.ToList();
    }

    public async Task<OrderResponse> UpdateOrder(OrderUpdateRequest orderUpdateRequest)
    {
        if (orderUpdateRequest == null)
        {
            throw new ArgumentException(nameof(orderUpdateRequest));
        }

        // Validate the orderupdate request
        ValidationResult OrderUpdatevalidationResult = await _OrderUpdaterequestValidator.ValidateAsync(orderUpdateRequest);
        if (!OrderUpdatevalidationResult.IsValid)
        {
            string error = string.Join(",", OrderUpdatevalidationResult.Errors.Select(temp => temp.ErrorMessage));
            throw new ArgumentException(error);
        }

        // Validate order items
        foreach (OrderItemUpdateRequest updateRequest in orderUpdateRequest.OrderItems)
        {
            ValidationResult OrderItemUpdateResult = await _OrderItemUpdaterequestValidator.ValidateAsync(updateRequest);
            if (!OrderItemUpdateResult.IsValid)
            {
                string error = string.Join(",", OrderItemUpdateResult.Errors.Select(temp => temp.ErrorMessage));
                throw new ArgumentException(error);
            }
        }

        // Check if user exists
        UserDTO user = await _usersMicroServiceClinet.GetUserByUserId(orderUpdateRequest.UserID);
        if (user == null)
        {
            throw new ArgumentException("Invalid user Id");
        }

        Order orderInput = _mapper.Map<Order>(orderUpdateRequest);

        // Calculate total values
        foreach (OrderItem orderItem in orderInput.OrderItems)
        {
            orderItem.TotalPrice = orderItem.Quantity * orderItem.UnitPrice;
        }

        orderInput.TotalBill = orderInput.OrderItems.Sum(temp => temp.TotalPrice);

        // Update order
        Order updatedOrder = await _iorderRepository.UpdateOrder(orderInput);

        if (updatedOrder == null)
        {
            return null;
        }

        // Map to response
        OrderResponse orderUpdateResponse = _mapper.Map<OrderResponse>(updatedOrder);
        return orderUpdateResponse;
    }
}
