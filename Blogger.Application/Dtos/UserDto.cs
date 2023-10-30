using Blogger.Application.Common.Mapping;
using Blogger.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blogger.Application.Dtos
{
    public class UserDto : IMapFrom<User>
    {
		public int Id { get; set; }
		[Required]
		public string Email { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Gender { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public string? Address { get; set; }
		//public string RefreshToken { get; set; }
		public bool IsDeleted { get; set; }
		//public List<Comment> Comments { get; set; }
		//public List<Post> Posts { get; set; }
		//public List<UserRole> UserRoles { get; set; }
	}
}
