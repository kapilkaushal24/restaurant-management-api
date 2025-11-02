using AutoMapper;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Domain.Entities;
using RestaurantManagement.Domain.Enums;
using RestaurantManagement.Domain.Interfaces;

namespace RestaurantManagement.Application.Services;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;

    public AuthService(IRepository<User> userRepository, IJwtService jwtService, IMapper mapper)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        // Check if user already exists
        var existingUser = await _userRepository.FindAsync(u => u.Email == registerDto.Email);
        if (existingUser.Any())
            throw new Exception("User with this email already exists");

        // Parse role
        if (!Enum.TryParse<UserRole>(registerDto.Role, true, out var userRole))
            throw new Exception("Invalid role");

        // Create user
        var user = new User
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
            Role = userRole,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);

        // Generate tokens
        var token = _jwtService.GenerateToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        var response = _mapper.Map<AuthResponseDto>(user);
        response.Token = token;
        response.RefreshToken = refreshToken;

        return response;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var users = await _userRepository.FindAsync(u => u.Email == loginDto.Email);
        var user = users.FirstOrDefault();

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            throw new Exception("Invalid email or password");

        var token = _jwtService.GenerateToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        var response = _mapper.Map<AuthResponseDto>(user);
        response.Token = token;
        response.RefreshToken = refreshToken;

        return response;
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        var principal = _jwtService.GetPrincipalFromExpiredToken(refreshTokenDto.Token);
        if (principal == null)
            throw new Exception("Invalid token");

        var userIdClaim = principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            throw new Exception("Invalid token");

        var user = await _userRepository.GetByIdAsync(int.Parse(userIdClaim.Value));
        if (user == null)
            throw new Exception("User not found");

        var newToken = _jwtService.GenerateToken(user);
        var newRefreshToken = _jwtService.GenerateRefreshToken();

        var response = _mapper.Map<AuthResponseDto>(user);
        response.Token = newToken;
        response.RefreshToken = newRefreshToken;

        return response;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}
