using Blog_DevIO.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Blog_DevIO.Core.Data.Seed
{
    public static class DbMigrationHelperExtension
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            DbMigrationHelper.EnsureSeedData(app).Wait();
        }
    }
    public static class DbMigrationHelper
    {
        public static async Task EnsureSeedData(WebApplication application)
        {
            var services = application.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var context = scope.ServiceProvider.GetRequiredService<BlogContext>();

            if (env.IsDevelopment())
            {
                await context.Database.MigrateAsync();
                await EnsureSeedData(context);
            }
        }

        private static async Task EnsureSeedData(BlogContext context)
        {
            if (context.Post.Any() || context.Users.Any()) return;

            #region ADMIN SEED
            string ADMIN_ROLE_ID = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";
            await context.Roles.AddAsync(new IdentityRole
            {
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN",
                Id = ADMIN_ROLE_ID,
                ConcurrencyStamp = ADMIN_ROLE_ID
            });

            string ADMIN_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
            var adminUser = new User("Caio", "Silva")
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
            adminUser.PasswordHash = ph.HashPassword(adminUser, "Teste@123");
            await context.Users.AddAsync(adminUser);

            await context.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });


            #endregion

            #region USER SEED
            string ROLE_ID = "048a44ed-981e-4cdf-b79f-d4b473158362";
            await context.Roles.AddAsync(new IdentityRole
            {
                Name = "BlogUser",
                NormalizedName = "BLOGUSER",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            string USER_ID = "eb430f77-8705-454d-b9b3-2a2e3081610d";
            var user = new User("User", "Teste")
            {
                Id = USER_ID,
                Email = "blog@teste.com",
                EmailConfirmed = true,
                UserName = "blog@teste.com",
                NormalizedUserName = "blog@teste.com",
                NormalizedEmail = "blog@teste.com"
            };

            //set user password
            ph = new PasswordHasher<User>();
            user.PasswordHash = ph.HashPassword(user, "Teste@123");
            await context.Users.AddAsync(user);

            await context.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = USER_ID
            });


            #endregion

            await context.SaveChangesAsync();
        }
    }
}
