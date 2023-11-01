using Blogger.Application.Common.Mapping;
using Blogger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Application.Dtos
{
    public class UserRegistrationDto : IMapFrom<UserDto>
    {
        public int Id { get; set; } = 0;
        [Required(ErrorMessage = "Please enter Email.")]
        [EmailAddress(ErrorMessage = "Invalid Email. Please re-enter.")]
		[MaxLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
		public string Email { get; set; }
        [Required(ErrorMessage = "Please enter Password.")]
		[MaxLength(100, ErrorMessage = "Password cannot be longer than 100 characters.")]
		[MinLength(6, ErrorMessage = "Password is not enough length. Please re-enter.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter Password.")]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please enter First Name.")]
        [Display(Name = "First Name")]
		[MaxLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
		public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter Last Name.")]
        [Display(Name = "Last Name")]
		[MaxLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
		public string LastName { get; set; }

        [Required(ErrorMessage = "Please choose Gender.")]
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; } = DateTime.Now;
        public string? Address { get; set; }
        public bool IsDeleted { get; set; }
    }
}
