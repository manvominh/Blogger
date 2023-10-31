using Blogger.Application.Common.Mapping;
using Blogger.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blogger.Application.Dtos
{
    public class RoleDto : IMapFrom<Role>
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Please enter Role Name.")]
		[MaxLength(50, ErrorMessage = "Role Name cannot be longer than 50 characters.")]
		public string Name { get; set; }
    }
}
