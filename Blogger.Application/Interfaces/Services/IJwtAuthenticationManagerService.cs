using Blogger.Application.Dtos;
using Blogger.Domain.Entities;
using System.Security.Claims;

namespace Blogger.Application.Interfaces.Services
{
    public interface IJwtAuthenticationManagerService
    {
        Task<UserSession?> GenerateJwtToken(User user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
