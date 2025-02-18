using eCommerce.Core.Entities;
namespace eCommerce.Core.RepositoryContracts
{
    public interface IusersRepository
    {
        //Methord to add user dara and retuen the added user
        Task<ApplicationUser?>AddUser(ApplicationUser user);
        //Methord to get exiting User By Email And Password 
        Task<ApplicationUser?>GetUserByEmailAndPassword(string? email, string? password);
    }
}
