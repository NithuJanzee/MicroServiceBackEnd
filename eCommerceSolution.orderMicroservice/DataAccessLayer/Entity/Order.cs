using MongoDB.Bson.Serialization.Attributes;

namespace eCommerce.OrderMicroservice.DataAccessLayer.Entity;

public class Order
{
    //mongo save guid as a binary data so we fore the mongo save the guid as a string
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid _id { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid OrderID { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid UserID { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalBill { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


}   
 