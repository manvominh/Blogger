using Blogger.Application.Dtos;
using Blogger.Domain.Entities;

namespace Blogger.Application.Interfaces.Services
{
    public interface IJwtAuthenticationManagerService
    {
        Task<UserSession?> GenerateJwtToken(User user);
    }
}
