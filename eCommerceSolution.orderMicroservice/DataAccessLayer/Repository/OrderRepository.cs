using eCommerce.OrderMicroservice.DataAccessLayer.Entity;
using eCommerce.OrderMicroservice.DataAccessLayer.RepositoryContracts;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace eCommerce.OrderMicroservice.DataAccessLayer.Repository;

public class OrderRepository : IorderRepository
{
    private readonly IMongoCollection<Order> _orders;
    private readonly string collectionName = "orders";

    public OrderRepository(IMongoDatabase mongoDatabase)
    {
        _orders = mongoDatabase.GetCollection<Order>(collectionName);
    }

    public async Task<Order?> AddOrder(Order order)
    {
        order.OrderID = Guid.NewGuid();
        order._id = order.OrderID;

        foreach (OrderItem orderItem in order.OrderItems)
        {
            orderItem._Id = Guid.NewGuid();
        }
        await _orders.InsertOneAsync(order);
        return order;
    }

    public async Task<bool> DeleteOrder(Guid orderId)
    {
        FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(x => x.OrderID, orderId);
        Order? ExitingOrder = (await _orders.FindAsync(filter)).FirstOrDefault();
        if (ExitingOrder == null)
        {
            return false;
        }

        DeleteResult delete = await _orders.DeleteOneAsync(filter);
        return delete.DeletedCount > 0;
    }

    public async Task<Order?> GetOrderByCondition(FilterDefinition<Order> filter)
    {
        return (await _orders.FindAsync(filter)).FirstOrDefault();
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        return (await _orders.FindAsync(Builders<Order>.Filter.Empty)).ToList();
    }

    public async Task<IEnumerable<Order?>> GetOrdersByCondition(FilterDefinition<Order> filter)
    {
        return (await _orders.FindAsync(filter)).ToList();
    }

    public async Task<Order?> UpdateOrder(Order order)
    {
        FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(x => x.OrderID, order.OrderID);
        Order? ExitingOrder = (await _orders.FindAsync(filter)).FirstOrDefault();
        if (ExitingOrder == null)
        {
            return null;
        }

        order._id = ExitingOrder._id;

        ReplaceOneResult replaceOneResult = await _orders.ReplaceOneAsync(filter, order);
        return order;
    }
}
