using AutoMapper;
using eCommerce.OrderMicroservice.Businesslogiclayer.HttpClients;
using eCommerce.OrderMicroservice.Businesslogiclayer.ServiceContact;
using eCommerce.OrderMicroservice.BusinessLogicLayer.DTO;
using eCommerce.OrderMicroservice.DataAccessLayer.Entity;
using eCommerce.OrderMicroservice.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using MongoDB.Driver;
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

    public OrderService(IValidator<OrderAddRequest> orderAddrequestValidator, IValidator<OrderItemAddRequest> orderItemAddrequestValidator, IValidator<OrderItemUpdateRequest> orderItemUpdaterequestValidator, IValidator<OrderUpdateRequest> orderUpdaterequestValidator, IorderRepository iorderRepository, IMapper mapper, UsersMicroServiceClint usersMicroService)
    {
        _OrderAddrequestValidator = orderAddrequestValidator;
        _OrderItemAddrequestValidator = orderItemAddrequestValidator;
        _OrderItemUpdaterequestValidator = orderItemUpdaterequestValidator;
        _OrderUpdaterequestValidator = orderUpdaterequestValidator;
        _iorderRepository = iorderRepository;
        _mapper = mapper;
        _usersMicroServiceClinet = usersMicroService;
    }

    public async Task<OrderResponse?> AddOrder(OrderAddRequest orderAddRequest)
    {
        if (orderAddRequest == null)
        {
            throw new ArgumentNullException(nameof(orderAddRequest));
        }

        // validate using fluent validation
        ValidationResult OrderAddvalidationResult = await _OrderAddrequestValidator.ValidateAsync(orderAddRequest);
        if (!OrderAddvalidationResult.IsValid)
        {
            string error = string.Join(",", OrderAddvalidationResult.Errors.Select(temp => temp.ErrorMessage));
            throw new ArgumentException(error);
        }
        //validate order items using fluent validation
        foreach (OrderItemAddRequest orderItemAddRequest in orderAddRequest.OrderItems)
        {
            ValidationResult validationResult = await _OrderItemAddrequestValidator.ValidateAsync(orderItemAddRequest);
            if (!validationResult.IsValid)
            {
                string errors = string.Join(",", validationResult.Errors.Select(temp => temp.ErrorMessage));
                throw new ArgumentException(errors);
            }
        }

        //TO DO: Add logic for checking if UserID exists in Users microservice
        // beacuse all data manage by another microservice sooooo we need to check if the user exists or not
        UserDTO? user = await _usersMicroServiceClinet.GetUserByUserId(orderAddRequest.UserId);
        if (user == null)
        {
            throw new ArgumentException("Invalid user Id");
        }


        Order orderInput = _mapper.Map<Order>(orderAddRequest);
        // genrate total value 
        foreach (OrderItem orderItem in orderInput.OrderItems)
        {
            orderItem.TotalPrice = (double)(orderItem.Quantity * orderItem.UnitPrice);
        }
        orderInput.TotalBill = (decimal)orderInput.OrderItems.Sum(temp => temp.TotalPrice);
        //Invoke repository
        Order? addedOrder = await _iorderRepository.AddOrder(orderInput);

        if (addedOrder == null)
        {
            return null;
        }
        //Map addedOrder ('Order' type) into 'OrderResponse' type (it invokes OrderToOrderResponseMappingProfile).
        OrderResponse addedOrderResponse = _mapper.Map<OrderResponse>(addedOrder);

        return addedOrderResponse;
    }


    public async Task<bool> DeleteOrder(Guid orderID)
    {
        FilterDefinition<Order> Filter = Builders<Order>.Filter.Eq(temp => temp.OrderID, orderID);
        Order? exitingOrder = await _iorderRepository.GetOrderByCondition(Filter);
        if (exitingOrder == null)
        {
            return false;
        }

        bool isDeleted = await _iorderRepository.DeleteOrder(orderID);
        return isDeleted;
    }

    public async Task<OrderResponse?> GetOrderByCondition(FilterDefinition<Order> filter)
    {
        Order? order = await _iorderRepository.GetOrderByCondition(filter);
        if (order == null)
        {
            return null;
        }

        OrderResponse? response = _mapper.Map<OrderResponse?>(order);
        return response;
    }

    public async Task<List<OrderResponse?>> GetOrders()
    {
        IEnumerable<Order> orders = await _iorderRepository.GetOrders();
        IEnumerable<OrderResponse?> OrderResponse = _mapper.Map<IEnumerable<OrderResponse?>>(orders);
        return OrderResponse.ToList();
    }

    public async Task<List<OrderResponse?>> GetOrdersByCondition(FilterDefinition<Order> filter)
    {
        IEnumerable<Order?> order = await _iorderRepository.GetOrdersByCondition(filter);
        if (order == null)
        {
            return null;
        }

        IEnumerable<OrderResponse?> Response = _mapper.Map<IEnumerable<OrderResponse>>(order);
        return Response.ToList();
    }

    public async Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest)
    {
        if (orderUpdateRequest == null)
        {
            throw new ArgumentException(nameof(orderUpdateRequest));
        }

        // validate the orderupdate request
        ValidationResult OrderUpdatevalidationResult = await _OrderUpdaterequestValidator.ValidateAsync(orderUpdateRequest);
        if (!OrderUpdatevalidationResult.IsValid)
        {
            string error = string.Join(",", OrderUpdatevalidationResult.Errors.Select(temp => temp.ErrorMessage));
            throw new ArgumentException(error);
        }

        //inside the orderupdate request validate the order item update item request
        foreach (OrderItemUpdateRequest updateRequest in orderUpdateRequest.OrderItems)
        {
            ValidationResult OrderItemUpdateResult = await _OrderItemUpdaterequestValidator.ValidateAsync(updateRequest);
            if (!OrderItemUpdateResult.IsValid)
            {
                string error = string.Join(",", OrderItemUpdateResult.Errors.Select(temp => temp.ErrorMessage));
                throw new ArgumentException(error);
            }
        }

        //TO DO: Add logic for checking if UserID exists in Users microservice
        UserDTO? user = await _usersMicroServiceClinet.GetUserByUserId(orderUpdateRequest.UserID);
        if (user == null)
        {
            throw new ArgumentException("Invalid user Id");
        }


        Order OrderInput = _mapper.Map<Order>(orderUpdateRequest);

        // Genrate total value
        foreach (OrderItem orderItem in OrderInput.OrderItems)
        {
            orderItem.TotalPrice = (double)(orderItem.Quantity * orderItem.UnitPrice);
        }
        OrderInput.TotalBill = (decimal)OrderInput.OrderItems.Sum(temp => temp.TotalPrice);

        //Invoke repository
        Order? OrderRepository = await _iorderRepository.UpdateOrder(OrderInput);
        if (OrderRepository == null)
        {
            return null;
        }
        OrderResponse orderUpdateResponse = _mapper.Map<OrderResponse>(OrderRepository);
        return orderUpdateResponse;
    }
}
