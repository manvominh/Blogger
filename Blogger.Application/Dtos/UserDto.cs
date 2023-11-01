using Blogger.Application.Common.Mapping;
using Blogger.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blogger.Application.Dtos
{
    public class UserDto : IMapFrom<User>
    {
		public int Id { get; set; }
		[Required(ErrorMessage ="Email can not be empty.")]
		[MaxLength(100,ErrorMessage = "Email cannot be longer than 100 characters.")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password can not be empty.")]
		public string Password { get; set; }
		[Required(ErrorMessage = "First Name can not be empty.")]
		[MaxLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Last Name can not be empty.")]
		[MaxLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "Gender can not be empty.")]
		[MaxLength(10, ErrorMessage = "Gender cannot be longer than 10 characters.")]
		public string Gender { get; set; }
		public DateTime? DateOfBirth { get; set; }
        [MaxLength(800, ErrorMessage = "Address cannot be longer than 800 characters.")]
        public string? Address { get; set; }
		//public string RefreshToken { get; set; }
		public bool IsDeleted { get; set; }
        public List<UserRole>? UserRoles { get; set; }
    }
}
