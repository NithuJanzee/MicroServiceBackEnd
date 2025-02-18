namespace eCommerce.Core.DTO
{
    public record RegistorRequestDTO(
        string? Email,
        string? Password,
        string? PersonName,
        GenderOptions Gender
        );
    
}
