using System.Linq.Expressions;
using eCommerce.OrderMicroservice.BusinessLogicLayer.DTO;
using eCommerce.OrderMicroservice.DataAccessLayer.Entity;

namespace eCommerce.OrderMicroservice.Businesslogiclayer.ServiceContact;

public interface IorderService
{
    Task<List<OrderResponse>> GetOrders();
    Task<List<OrderResponse>> GetOrdersByCondition(Expression<Func<Order, bool>> predicate);
    Task<OrderResponse> GetOrderByCondition(Expression<Func<Order, bool>> predicate);
    Task<OrderResponse> AddOrder(OrderAddRequest orderAddRequest);
    Task<OrderResponse> UpdateOrder(OrderUpdateRequest orderUpdateRequest);
    Task<bool> DeleteOrder(Guid orderID);
}
