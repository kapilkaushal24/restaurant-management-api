using RestaurantManagement.Domain.Entities;
using System.Security.Claims;

namespace RestaurantManagement.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
