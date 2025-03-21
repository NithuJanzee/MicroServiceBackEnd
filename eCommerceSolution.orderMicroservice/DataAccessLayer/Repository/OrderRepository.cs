using System.Linq.Expressions;
using eCommerce.DataAccessLayer.DbContext1;
using eCommerce.OrderMicroservice.DataAccessLayer.Entity;
using eCommerce.OrderMicroservice.DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.OrderMicroservice.DataAccessLayer.Repository;

public class OrderRepository : IorderRepository
{
    private readonly OrderDbContext _dbContext;

    public OrderRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Order> AddOrder(Order order)
    {
        order.OrderID = Guid.NewGuid();

        foreach (var orderItem in order.OrderItems)
        {
            orderItem.OrderItemID = Guid.NewGuid();
            orderItem.OrderID = order.OrderID;
        }

        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<bool> DeleteOrder(Guid orderId)
    {
        var order = await _dbContext.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderID == orderId);

        if (order == null)
        {
            return false;
        }

        _dbContext.OrderItems.RemoveRange(order.OrderItems);
        _dbContext.Orders.Remove(order);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Order> GetOrderByCondition(Expression<Func<Order, bool>> predicate)
    {
        return await _dbContext.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        return await _dbContext.Orders
            .Include(o => o.OrderItems)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByCondition(Expression<Func<Order, bool>> predicate)
    {
        return await _dbContext.Orders
            .Include(o => o.OrderItems)
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<Order> UpdateOrder(Order order)
    {
        var existingOrder = await _dbContext.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderID == order.OrderID);

        if (existingOrder == null)
        {
            return null;
        }

        // Remove existing order items
        _dbContext.OrderItems.RemoveRange(existingOrder.OrderItems);

        // Update order properties
        _dbContext.Entry(existingOrder).CurrentValues.SetValues(order);

        // Add new order items
        foreach (var item in order.OrderItems)
        {
            item.OrderItemID = Guid.NewGuid();
            item.OrderID = order.OrderID;
            _dbContext.OrderItems.Add(item);
        }

        await _dbContext.SaveChangesAsync();
        return existingOrder;
    }
}

