using System.ComponentModel.DataAnnotations;
using AutoMapper;
using eCommerce.OrderMicroservice.Businesslogiclayer.ServiceContact;
using eCommerce.OrderMicroservice.Businesslogiclayer.Validators;
using eCommerce.OrderMicroservice.BusinessLogicLayer.DTO;
using eCommerce.OrderMicroservice.Businesslogiclayerr.Validators;
using eCommerce.OrderMicroservice.DataAccessLayer.Entity;
using eCommerce.OrderMicroservice.DataAccessLayer.Repository;
using eCommerce.OrderMicroservice.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using Microsoft.Extensions.Options;
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

    public OrderService(IValidator<OrderAddRequest> orderAddrequestValidator, IValidator<OrderItemAddRequest> orderItemAddrequestValidator, IValidator<OrderItemUpdateRequest> orderItemUpdaterequestValidator, IValidator<OrderUpdateRequest> orderUpdaterequestValidator, IorderRepository iorderRepository, IMapper mapper)
    {
        _OrderAddrequestValidator = orderAddrequestValidator;
        _OrderItemAddrequestValidator = orderItemAddrequestValidator;
        _OrderItemUpdaterequestValidator = orderItemUpdaterequestValidator;
        _OrderUpdaterequestValidator = orderUpdaterequestValidator;
        _iorderRepository = iorderRepository;
        _mapper = mapper;
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



        Order orderInput = _mapper.Map<Order>(orderAddRequest);
        // genrate total value 
        foreach (OrderItem orderItem in orderInput.OrderItem)
        {
            orderItem.TotalPrice = (double)(orderItem.Quantity * orderItem.UnitPrice);
        }
        orderInput.TotalBill = (decimal)orderInput.OrderItem.Sum(temp => temp.TotalPrice);
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
        FilterDefinition<Order> Filter = Builders<Order>.Filter.Eq(temp => temp.OrderId, orderID);
        Order? exitingOrder = await _iorderRepository.GetOrderByCondition(Filter);
        if (exitingOrder == null)
        {
            return false;
        }

        bool isDeleted = await _iorderRepository.DeleteOrder(orderID);
        return isDeleted;
    }

    public Task<OrderResponse?> GetOrderByCondition(FilterDefinition<Order> filter)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderResponse?>> GetOrders()
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderResponse?>> GetOrdersByCondition(FilterDefinition<Order> filter)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest)
    {
        if(orderUpdateRequest == null)
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
        foreach(OrderItemUpdateRequest updateRequest in orderUpdateRequest.OrderItems)
        {
            ValidationResult OrderItemUpdateResult = await _OrderItemUpdaterequestValidator.ValidateAsync(updateRequest);
            if(!OrderItemUpdateResult.IsValid)
            {
                string error = string.Join(",", OrderItemUpdateResult.Errors.Select(temp => temp.ErrorMessage));
                throw new ArgumentException(error);
            }
        }

        //TO DO: Add logic for checking if UserID exists in Users microservice


        Order OrderInput = _mapper.Map<Order>(orderUpdateRequest);

        // Genrate total value
        foreach(OrderItem orderItem in OrderInput.OrderItem)
        {
            orderItem.TotalPrice = (double)(orderItem.Quantity * orderItem.UnitPrice);
        }
        OrderInput.TotalBill = (decimal)OrderInput.OrderItem.Sum(temp => temp.TotalPrice);

        //Invoke repository
        Order? OrderRepository = await _iorderRepository.UpdateOrder(OrderInput);
        if(OrderRepository == null)
        {
            return null;
        }
        OrderResponse orderUpdateResponse = _mapper.Map<OrderResponse>(OrderRepository);
        return orderUpdateResponse;
    }
}
