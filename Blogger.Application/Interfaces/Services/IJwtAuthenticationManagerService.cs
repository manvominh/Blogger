using Blogger.Application.Dtos;
using Blogger.Domain.Entities;
using System.Security.Claims;

namespace Blogger.Application.Interfaces.Services
{
    public interface IJwtAuthenticationManagerService
    {
        Task<Tokens> GenerateJwtToken(User user);
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}
