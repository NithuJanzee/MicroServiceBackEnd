using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.OrderMicroservice.DataAccessLayer.Entity;

public class OrderItem
{

    [Key]
    public Guid OrderItemID { get; set; }
    public Guid ProductID { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }

    public Guid OrderID { get; set; }
    [ForeignKey("OrderID")]
    public virtual Order Order { get; set; }
}
