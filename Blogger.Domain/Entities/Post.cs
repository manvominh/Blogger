using Blogger.Domain.Common;

namespace Blogger.Domain.Entities
{
    public class Post : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Introduction { get; set; }
        public string BodyText { get; set; }
        public string Image { get; set; }
        public bool IsPublished { get; set; }
        public DateTime PublishedDate { get; set; }
        public int UserId { get; set; }
        public List<Comment> Comments { get; set; }
        public User User { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
