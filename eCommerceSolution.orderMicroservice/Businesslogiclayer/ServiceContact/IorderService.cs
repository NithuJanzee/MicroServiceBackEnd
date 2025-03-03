using eCommerce.OrderMicroservice.BusinessLogicLayer.DTO;
using MongoDB.Driver;
using eCommerce.OrderMicroservice.DataAccessLayer.Entity;

namespace eCommerce.OrderMicroservice.Businesslogiclayer.ServiceContact;

public interface IorderService
{
    Task<List<OrderResponse?>> GetOrders();
    Task<List<OrderResponse?>> GetOrdersByCondition(FilterDefinition<Order> filter);
    Task<OrderResponse?> GetOrderByCondition(FilterDefinition<Order> filter);
    Task<OrderResponse?> AddOrder(OrderAddRequest orderAddRequest);
    Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest);
    Task<bool> DeleteOrder(Guid orderID);
}
    