using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet]
    public async Task<IActionResult> GetUserByID(Guid id)
    {
        if(id == Guid.Empty) return BadRequest("Invalid User Id");
        UserDTO? user = await _userService.GetUserByID(id);
        if(user == null) return NotFound(user);
        return Ok(user);
    }
}
