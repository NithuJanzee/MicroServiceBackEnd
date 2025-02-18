using eCommerce.Core.DTO;

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
    }
}
