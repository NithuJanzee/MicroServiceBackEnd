

namespace BusinessLogicLayer.DTO;
public record ProductResponse(string ProductName, CategoryOptions Category, double? UnitPrice, int? QuantityInStock)
{
    ProductResponse() : this(default, default, default, default)
    {

    }
}