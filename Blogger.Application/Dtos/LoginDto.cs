using System.ComponentModel.DataAnnotations;

namespace Blogger.Application.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Please enter Email.")]
        [EmailAddress(ErrorMessage ="Invalid Email. Please re-enter.")]
		[MaxLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
		public string Email { get; set; }
        [Required(ErrorMessage = "Please enter Password.")]
        [MinLength(6, ErrorMessage ="Password is not enough length. Please re-enter.")]
		[MaxLength(100, ErrorMessage = "Password cannot be longer than 100 characters.")]
		public string Password { get; set; }
    }
}
