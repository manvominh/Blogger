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
            //            modelBuilder.Entity<UserRole>().HasKey(userRole => new { userRole.UserId, userRole.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne<User>(userRole => userRole.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(userRole => userRole.UserId);


            modelBuilder.Entity<UserRole>()
                .HasOne<Role>(userRole => userRole.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(userRole => userRole.RoleId);

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

            //$2a$11$QbzFNu6YonRwcvludZ0aKu6i6DmbXmUgiTHKZQHKLcSFug.jAg8bu
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
        }
    }
}
