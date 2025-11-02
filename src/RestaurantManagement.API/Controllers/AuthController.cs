using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;

namespace RestaurantManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var response = await _authService.RegisterAsync(registerDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Registration failed");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("register-bulk")]
    public async Task<ActionResult> RegisterBulk([FromBody] List<RegisterDto> registerDtos)
    {
        try
        {
            var results = new List<object>();
            var successCount = 0;
            var failureCount = 0;

            foreach (var registerDto in registerDtos)
            {
                try
                {
                    var response = await _authService.RegisterAsync(registerDto);
                    results.Add(new 
                    { 
                        email = registerDto.Email, 
                        success = true, 
                        message = "Registered successfully",
                        userId = response.UserId
                    });
                    successCount++;
                }
                catch (Exception ex)
                {
                    results.Add(new 
                    { 
                        email = registerDto.Email, 
                        success = false, 
                        message = ex.Message 
                    });
                    failureCount++;
                    _logger.LogWarning(ex, "Failed to register user: {Email}", registerDto.Email);
                }
            }

            return Ok(new 
            { 
                totalProcessed = registerDtos.Count,
                successCount,
                failureCount,
                results 
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bulk registration failed");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var response = await _authService.LoginAsync(loginDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login failed");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthResponseDto>> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
    {
        try
        {
            var response = await _authService.RefreshTokenAsync(refreshTokenDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Token refresh failed");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("users")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        try
        {
            var users = await _authService.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get users");
            return BadRequest(new { message = ex.Message });
        }
    }
}
