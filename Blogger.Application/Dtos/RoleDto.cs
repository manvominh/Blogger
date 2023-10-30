using Blogger.Application.Common.Mapping;
using Blogger.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blogger.Application.Dtos
{
    public class RoleDto : IMapFrom<Role>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
