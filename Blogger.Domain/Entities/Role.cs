using Blogger.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Blogger.Domain.Entities
{
    public class Role : IEntity
    {
        public int Id { get; set; }
		[Required]
		[StringLength(50)]
		public string Name { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
