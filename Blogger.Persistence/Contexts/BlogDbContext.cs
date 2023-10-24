using Blogger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Blogger.Persistence.Contexts
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.PostId)
                .IsRequired();
            
            // Role
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = 1,
                Name = "Administrator",
            });
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = 2,
                Name = "User",
            });

            // User
            modelBuilder.Entity<User>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Email = "admin@admin.com",
                Password = "$2a$11$IWQWEsJKjaOjDOCPTDbyKOZi/3D3jLT0OBSwcSpZ5gjDO8DH3V0ua",
                FirstName ="super",
                LastName ="admin",
                Gender = "Mr",
                DateOfBirth = DateTime.Now,
                Address = "Google Inc."
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 2,
                Email = "user@user.com",
                Password = "$2a$11$P6IdI8n.ZjUUADMhb1RswOOup1JxYjeHL4lDHtoyQCctOPHsKelJK",
                FirstName = "super",
                LastName = "user",
                Gender = "Mr",
                DateOfBirth = DateTime.Now,
                Address = "Google Inc."
            });
			// UserRole
			//            modelBuilder.Entity<UserRole>().HasKey(userRole => new { userRole.UserId, userRole.RoleId });
			modelBuilder.Entity<UserRole>()
				.HasOne<User>(userRole => userRole.User)
				.WithMany(r => r.UserRoles)
				.HasForeignKey(userRole => userRole.UserId);

			modelBuilder.Entity<UserRole>()
				.HasOne<Role>(userRole => userRole.Role)
				.WithMany(r => r.UserRoles)
				.HasForeignKey(userRole => userRole.RoleId);

			modelBuilder.Entity<UserRole>().HasData(new UserRole
            {
                Id = 1,
                UserId = 1,
                RoleId = 1,
            });
            modelBuilder.Entity<UserRole>().HasData(new UserRole
            {
                Id = 2,
                UserId = 1,
                RoleId = 2,
            });
            modelBuilder.Entity<UserRole>().HasData(new UserRole
            {
                Id = 3,
                UserId = 2,
                RoleId = 2,
            });

            // Post
            modelBuilder.Entity<Post>().HasData(new Post
            {
                Id = 1,
                Title = "How to implement a blog site by using Web API + Blazor Web Assembly + Clean Architecture",
                Introduction = "Blazor is a modern web user interface development technology developed by Microsoft. It allows for modern web application development, such as highly interactive single-page applications (SPAs).",
				BodyText = "Blazor uses HTML, CSS and C# to create interactive client-side web user interfaces. Instead of using Microsoft-specific technology such as XAML, it uses standard HTML and CSS to describe the user interface.\r\n\r\nAnd instead of JavaScript or TypeScript like React, Angular, Vue or other JavaScript web frameworks, Blazor uses C# to implement the application behavior.\r\n\r\nThe Blazor Component Model",
				Image = "img_1_sq.jpg",
                UserId = 1,
                CreatedDate = DateTime.Now.AddDays(-50),
                IsPublished = true,
                PublishedDate = DateTime.Now.AddDays(-44),
            });
			modelBuilder.Entity<Post>().HasData(new Post
			{
				Id = 2,
				Title = "Build optimized landing pages in minutes",
				Introduction = "With Mailchimp’s help, you can grow your audience and online presence with ease. Our library of templates is geared toward today’s users and are built by trusted experts",
				BodyText = "Start with a custom domain and get your business online now. Bring your free website and landing pages under one domain to keep your brand in harmony.\r\rnTest out your products, messaging, and ideas before you launch, so you can find your future customers, optimize your conversion rates, and get people excited about your brand.\r\n\r\nOur landing page builder pulls product imagery directly from your connected ecommerce store so you can feature a top seller or promote your latest collection.",
                Image = "img_2_sq.jpg",
				UserId = 1,
				CreatedDate = DateTime.Now.AddDays(-45),
				IsPublished = true,
				PublishedDate = DateTime.Now.AddDays(42),
			});
			modelBuilder.Entity<Post>().HasData(new Post
			{
				Id = 3,
				Title = "Connect to creative tools like Photoshop or Canva",
				Introduction = "With our library of 300+ integrations, you can create and import your existing templates into Mailchimp’s library, instantly.",
				BodyText = "Discover the apps you need to make running your business easier\r\nOur talented roster of Mailchimp experts can design and implement beautiful, data-driven templates for your brand. Not to mention help you with other marketing needs like connecting apps, creating automations, or creating an online store",
				Image = "img_3_sq.jpg",
				UserId = 1,
				CreatedDate = DateTime.Now.AddDays(-40),
				IsPublished = true,
				PublishedDate = DateTime.Now.AddDays(-38),
			});
			modelBuilder.Entity<Post>().HasData(new Post
			{
				Id = 4,
				Title = "Pre-built automations made to deliver ROI",
				Introduction = "With 90+ pre-built automation journeys built using marketing best practices, you can reach, engage, and convert more customers at scale. ",
				BodyText = "Nurture customers with an always-on presence. Scale and maintain more relevant interactions with messages that are specific to their needs and behaviors.\r\nWith automated emails built in Customer Journey Builder*, Mailchimp customers saw up to 127% increase in click rates compared to bulk emails.\r\nCampaign Manager lets you manage  your multichannel marketing efforts, like email and SMS*, all at once to help you save time, drive growth, and sell more.",
				Image = "img_4_sq.jpg",
				UserId = 2,
				CreatedDate = DateTime.Now.AddDays(-43),
				IsPublished = true,
				PublishedDate = DateTime.Now.AddDays(-40),
			});
			modelBuilder.Entity<Post>().HasData(new Post
			{
				Id = 5,
				Title = "Release your writer’s block using generative AI",
				Introduction = "Mailchimp’s robust content creation tools make writing compelling messaging (and writing it quickly) nearly effortless. For when you know what to say but need help on how to say it, ",
				BodyText = "Create content that drives engagement, builds trust, and earns loyalty\r\nOur industry-leading platform leverages millions of data points to help your team create unique, personalized marketing content.\r\nCreate compelling messaging in minutes with Generative AI\r\nGet ahead of the pack and stand out in your audience’s inbox. Mailchimp’s content creation features, like Generative AI, can turn your creative ideas into content that drives real returns on your investment. It’s easy and fast, so you can focus on the important things.*\r\n\r\n*This message was created by our Generative AI tool, but reviewed and posted by humans.\r\nNeed to hire a helping hand? We got you.\r\nFrom a quick template design to full-service campaign management, our global community of 850+ trusted experts does it all.",
				Image = "img_5_sq.jpg",
				UserId = 2,
				CreatedDate = DateTime.Now.AddDays(-30),
				IsPublished = true,
				PublishedDate = DateTime.Now.AddDays(-28),
			});
            // Comment
            modelBuilder.Entity<Comment>().HasData(new Comment
            {
                Id = 1,
                CommentText = "Amazing post. Blazor is the best web application right now.",
                PostId = 1,
                UserId = 1,
                CreatedDate = DateTime.Now,
            });
			// Comment
			modelBuilder.Entity<Comment>().HasData(new Comment
			{
				Id = 2,
				CommentText = "Lovely post.",
				PostId = 2,
				UserId = 1,
				CreatedDate = DateTime.Now,
			});
		}
    }
}
