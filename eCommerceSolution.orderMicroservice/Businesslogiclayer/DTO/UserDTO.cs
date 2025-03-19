namespace eCommerce.OrderMicroservice.BusinessLogicLayer.DTO;

public record class UserDTO(Guid UserID, string? Email, string? PersonName, string? Gender)
{
}