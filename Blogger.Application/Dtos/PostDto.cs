
using Blogger.Application.Common.Mapping;
using Blogger.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blogger.Application.Dtos
{
	public class PostDto : IMapFrom<Post>
	{
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Introduction { get; set; }
		//[Required]
		public string BodyText { get; set; }
		public string? Image { get; set; }
		public bool IsPublished { get; set; }
		public DateTime? PublishedDate { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
