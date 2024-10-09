using Blog_DevIO.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog_DevIO.Data
{
    public class BlogContext : IdentityDbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogContext).Assembly);
            CreateUserAdmin(modelBuilder);
            CreateBlogUsers(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void CreateUserAdmin(ModelBuilder modelBuilder)
        {
            string ADMIN_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
            string ROLE_ID = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";

            //seed admin role
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            //create user
            var appUser = new User("Caio", "Silva")
            {
                Id = ADMIN_ID,
                Email = "caiosilva@teste.com",
                EmailConfirmed = true,
                UserName = "caiosilva@teste.com",
                NormalizedUserName = "caiosilva@teste.com",
                NormalizedEmail = "caiosilva@teste.com"
            };

            //set user password
            PasswordHasher<User> ph = new PasswordHasher<User>();
            appUser.PasswordHash = ph.HashPassword(appUser, "Teste@123");

            //seed user
            modelBuilder.Entity<User>().HasData(appUser);

            //set user role to admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }

        private static void CreateBlogUsers(ModelBuilder modelBuilder)
        {
            string USER_ID = "eb430f77-8705-454d-b9b3-2a2e3081610d";
            string ROLE_ID = "048a44ed-981e-4cdf-b79f-d4b473158362";

            //seed admin role
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "BlogUser",
                NormalizedName = "BLOGUSER",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            //create user
            var appUser = new User("User", "Teste")
            {
                Id = USER_ID,
                Email = "blog@teste.com",
                EmailConfirmed = true,
                UserName = "blog@teste.com",
                NormalizedUserName = "blog@teste.com",
                NormalizedEmail = "blog@teste.com"
            };

            //set user password
            PasswordHasher<User> ph = new PasswordHasher<User>();
            appUser.PasswordHash = ph.HashPassword(appUser, "Teste@123");

            //seed user
            modelBuilder.Entity<User>().HasData(appUser);

            //set user role to admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = USER_ID
            });
        }
    }
}
