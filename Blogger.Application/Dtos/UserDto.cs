﻿using Blogger.Application.Common.Mapping;
using Blogger.Domain.Entities;

namespace Blogger.Application.Dtos
{
    public class UserDto : IMapFrom<User>
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        //public string RefreshToken { get; set; }
    }
}
