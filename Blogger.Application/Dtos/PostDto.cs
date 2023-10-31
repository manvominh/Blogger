
using Blogger.Application.Common.Mapping;
using Blogger.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Blogger.Application.Dtos
{
	public class PostDto : IMapFrom<Post>
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Please enter Title.")]
		[MaxLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
		public string Title { get; set; }
		[Required(ErrorMessage = "Please enter Introduction.")]
		[MaxLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
		public string Introduction { get; set; }
		public string BodyText { get; set; }
		public string ImageUrl { get; set; }
		public bool IsPublished { get; set; }
		public DateTime? PublishedDate { get; set; }
		public int UserId { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
