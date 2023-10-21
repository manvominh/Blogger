using Blogger.Domain.Entities;

namespace Blogger.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByEmail(string email);
    }
}
