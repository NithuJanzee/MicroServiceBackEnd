using System.Linq.Expressions;
using eCommerce.OrderMicroservice.DataAccessLayer.Entity;

namespace eCommerce.OrderMicroservice.DataAccessLayer.RepositoryContracts;

public interface IorderRepository
{
    Task<IEnumerable<Order>> GetOrders();
    Task<IEnumerable<Order>> GetOrdersByCondition(Expression<Func<Order, bool>> predicate);
    Task<Order> GetOrderByCondition(Expression<Func<Order, bool>> predicate);
    Task<Order> AddOrder(Order order);
    Task<Order> UpdateOrder(Order order);
    Task<bool> DeleteOrder(Guid orderId);

}
