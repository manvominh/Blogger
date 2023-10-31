using Blogger.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Blogger.Domain.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		[StringLength(50)]
		public string FirstName { get; set; }
		[Required]
		[StringLength(50)]
		public string LastName { get; set; }
		[Required]
		[StringLength(10)]
		public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [StringLength(800)]
        public string? Address { get; set; }
        //public string RefreshToken { get; set; }
        public bool IsDeleted { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Post> Posts { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
