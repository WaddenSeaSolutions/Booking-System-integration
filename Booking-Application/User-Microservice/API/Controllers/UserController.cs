using Microsoft.AspNetCore.Mvc;
using User_Microservice.Domain.Models;
using User_Microservice.Domain.Services;
using User_Microservice.Domain.DTO;

namespace User_Microservice.API.Controllers;

public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            User user = _userService.Register(request.Username, request.Email, request.Password);

            return Ok(new { user.Id, user.Username, user.Email });
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
