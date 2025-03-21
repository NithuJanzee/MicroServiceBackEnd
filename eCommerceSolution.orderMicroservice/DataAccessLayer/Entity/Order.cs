using System.ComponentModel.DataAnnotations;

namespace eCommerce.OrderMicroservice.DataAccessLayer.Entity;

public class Order
{
    [Key]
    public Guid OrderID { get; set; }
    public Guid UserID { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalBill { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

}
