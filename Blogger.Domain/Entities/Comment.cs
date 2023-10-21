﻿using Blogger.Domain.Common;

namespace Blogger.Domain.Entities
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }
        public DateTime Created { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}