using System.Threading.Tasks;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;

namespace eCommerce.Core.ServiceContracts
{
    //contract for the user service that contains the use cases fot the user    
    public interface IUserService
    {
        /// <summary>
        /// Methord to handle the User Login 
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        Task<AuthenticationResponse?> Login(LoginRequestDTO loginRequest);
        /// <summary>
        /// Methord to handle the user registration
        /// </summary>
        /// <param name="registor"></param>
        /// <returns></returns>
        Task<AuthenticationResponse?> Register(RegistorRequestDTO registor);


        /// <summary>
        /// Methord to get user by ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns> User DTO Object based on the matching user ID </returns>
        Task<UserDTO?> GetUserByID(Guid ID);
    }
}
