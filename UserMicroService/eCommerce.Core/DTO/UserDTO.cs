namespace eCommerce.Core.DTO;

public record class UserDTO(Guid UserID, string? Email, string? PersonName, string? Gender)
{
}
