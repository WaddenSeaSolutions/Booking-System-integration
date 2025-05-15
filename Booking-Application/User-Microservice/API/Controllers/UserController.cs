using Microsoft.AspNetCore.Mvc;
using User_Microservice.Applications.Interfaces;
using User_Microservice.Domain.Models;
using User_Microservice.Domain.Services;
using User_Microservice.Domain.DTO;

namespace User_Microservice.API.Controllers;

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        try
        {
            User registeredUser = _userService.Register(user);

            return Ok(new { registeredUser.Id, registeredUser.Username, registeredUser.Email });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred during registration.", details = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            User user = _userService.Login(request.Username, request.Password);

            return Ok(new { user.Id, user.Username, user.Email });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred during login.", details = ex.Message });
        }
    }
}
