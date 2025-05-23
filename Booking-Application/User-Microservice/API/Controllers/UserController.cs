using Microsoft.AspNetCore.Mvc;
using User_Microservice.Applications.Interfaces;
using User_Microservice.Domain.Models;
using User_Microservice.Domain.Services;
using User_Microservice.Domain.DTO;

namespace User_Microservice.API.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public UserController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
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

            var token = _tokenService.GenerateToken(user.Id.ToString(), user.Username);

            return Ok(new { user.Id, user.Username, user.Email, Token = token });
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
