
namespace BusinessLogicLayer.DTO;

public record ProductAddRequest(string ProductName, CategoryOptions Category, double? UnitPrice, int? QuantityInStock)
{
    ProductAddRequest():this(default , default, default, default)
    {

    }
}
