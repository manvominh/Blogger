using Blogger.Application.Dtos;
using System.Security.Claims;

namespace Blogger.Application.Interfaces.Services
{
    public interface IJwtAuthenticationManagerService
    {
        Task<Tokens> GenerateJwtToken(UserDto user);
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}
