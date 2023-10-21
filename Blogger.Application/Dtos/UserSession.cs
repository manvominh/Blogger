
namespace Blogger.Application.Dtos
{
    public class UserSession
    {
        public string FullName { get; set; }
        public string Token { get; set; }
        public List<string> RoleNames { get; set; }
        public int ExpiresIn { get; set; }
        public DateTime ExpiryTimeStamp { get; set; }
    }
}
