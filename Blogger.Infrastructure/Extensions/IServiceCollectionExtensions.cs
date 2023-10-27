using Blogger.Application.Interfaces.Services;
using Blogger.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Blogger.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddInfrastructureServiceLayer(this IServiceCollection services)
        {
            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IJwtAuthenticationManagerService, JwtAuthenticationManagerService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IPostService, PostService>()
				.AddTransient<IRoleService, RoleService>()
                .AddTransient<ICommentService, CommentService>()
                ;
        }
    }
}
