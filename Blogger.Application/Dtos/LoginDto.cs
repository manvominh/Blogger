using System.ComponentModel.DataAnnotations;

namespace Blogger.Application.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Please enter Email.")]
        [EmailAddress(ErrorMessage ="Invalid Email. Please re-enter.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter Password.")]
        [MinLength(6, ErrorMessage ="Password is not enough length. Please re-enter.")]
        public string Password { get; set; }
    }
}
