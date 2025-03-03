using eCommerce.OrderMicroservice.DataAccessLayer.Entity;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccessLayer.Entity;

public class Order
{
    //mongo save guid as a binary data so we fore the mongo save the guid as a string
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid OrderId { get; set; }
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalBill { get; set; }
    public List<OrderItem> OrderItem { get; set; } = new List<OrderItem>();


}   
 