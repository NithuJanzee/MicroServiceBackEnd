using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _UserService;

        public AuthController(IUserService userService)
        {
            _UserService = userService;
        }

        [HttpPost("Registor")]
        public async Task<IActionResult?> Registor(RegistorRequestDTO requestDTO)
            {
            if (requestDTO == null)
            {
                return BadRequest("InvalidRegistorData");
            }

            AuthenticationResponse? authenticationResponse = await _UserService.Register(requestDTO);
            if (authenticationResponse == null || authenticationResponse.success == false)
            {
                return BadRequest(authenticationResponse);
            }

            return Ok(authenticationResponse);

        }

        [HttpPost("Login")]
        public async Task<IActionResult?> Login(LoginRequestDTO loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Invalid Data For Login");
            }

            AuthenticationResponse? authenticationResponse = await _UserService.Login(loginRequest);

            if (authenticationResponse == null || authenticationResponse.success == false)
            {
                return Unauthorized(authenticationResponse);
            }
            return Ok(authenticationResponse);
        }
    }
}
