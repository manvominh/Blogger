using Blogger.Domain.Common;

namespace Blogger.Domain.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        //public string RefreshToken { get; set; }
        public bool IsDeleted { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Post> Posts { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
