using Blog_DevIO.Core.Entities;
using Blog_DevIO.Core.Services.Abstractions;
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
            if (context.Posts.Any() || context.Users.Any()) return;

            #region ADMIN SEED
            string ADMIN_ROLE_ID = Guid.NewGuid().ToString();
            await context.Roles.AddAsync(new IdentityRole
            {
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN",
                Id = ADMIN_ROLE_ID,
                ConcurrencyStamp = ADMIN_ROLE_ID
            });

            string ADMIN_ID = Guid.NewGuid().ToString();
            var adminUser = new IdentityUser
            {
                Id = ADMIN_ID,
                Email = "caiosilva@teste.com",
                EmailConfirmed = true,
                UserName = "caiosilva@teste.com",
                NormalizedUserName = "caiosilva@teste.com",
                NormalizedEmail = "caiosilva@teste.com"
            };

            //set user password
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            adminUser.PasswordHash = ph.HashPassword(adminUser, "Teste@123");
            await context.Users.AddAsync(adminUser);

            var author = new Author(Guid.Parse(adminUser.Id), "Caio", "Silva");
            await context.Authors.AddAsync(author);

            await context.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });


            #endregion

            #region USER SEED
            string ROLE_ID = Guid.NewGuid().ToString();
            await context.Roles.AddAsync(new IdentityRole
            {
                Name = "BlogUser",
                NormalizedName = "BLOGUSER",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            string USER_ID = Guid.NewGuid().ToString();
            var user = new IdentityUser
            {
                Id = USER_ID,
                Email = "blog@teste.com",
                EmailConfirmed = true,
                UserName = "blog@teste.com",
                NormalizedUserName = "blog@teste.com",
                NormalizedEmail = "blog@teste.com"
            };

            //set user password
            ph = new PasswordHasher<IdentityUser>();
            user.PasswordHash = ph.HashPassword(user, "Teste@123");
            await context.Users.AddAsync(user);

            var author2 = new Author(Guid.Parse(user.Id), "User", "Teste");
            await context.Authors.AddAsync(author2);

            await context.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = USER_ID
            });


            #endregion

            #region POST SEED         

            var post = new Post("Test Post",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean justo enim, ullamcorper sed erat mattis, hendrerit maximus neque. Phasellus pharetra euismod metus, ut mattis lectus. Pellentesque tempus, ligula bibendum feugiat rhoncus, turpis nisi dapibus metus, et elementum ipsum orci eu nibh. Curabitur congue ut sem et bibendum. Phasellus luctus tortor vitae erat sodales vulputate. Ut ut ante ac mi consequat sollicitudin quis ut nisl. Sed et nibh a metus dignissim dapibus ut ut nisl.\r\n\r\nPellentesque eget lobortis erat, non commodo tortor. Vivamus blandit vitae erat sed sollicitudin. Mauris faucibus ut metus vel lobortis. Vivamus lacus ex, tincidunt vitae lectus id, mattis venenatis nibh. Etiam ullamcorper ipsum quis quam consequat tincidunt et eget turpis. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Phasellus condimentum augue ipsum, vel pulvinar elit auctor quis. Phasellus mollis, lacus at vestibulum facilisis, tortor ex posuere ipsum, ut tincidunt arcu quam ac lorem. Nunc sit amet elementum velit. Aliquam condimentum, elit nec dignissim laoreet, diam felis sagittis est, et bibendum ex nisi fermentum nulla. Cras nec tempus dolor. Ut eleifend aliquam nisl, et imperdiet arcu. Sed accumsan felis commodo est venenatis porta. Nunc facilisis mauris tortor, et blandit eros maximus vehicula. Cras hendrerit libero et odio elementum aliquam.",
                author2.Id);

            await context.Posts.AddAsync(post);

            #endregion

            #region COMMENT SEED         

            var comment = new Comment("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean justo enim, ullamcorper sed erat mattis, hendrerit maximus neque. Phasellus pharetra euismod metus, ut mattis lectus. Pellentesque tempus, ligula bibendum feugiat rhoncus, turpis nisi dapibus metus, et elementum ipsum orci eu nibh. Curabitur congue ut sem et bibendum. Phasellus luctus tortor vitae erat sodales vulputate. Ut ut ante ac mi consequat sollicitudin quis ut nisl. Sed et nibh a metus dignissim dapibus ut ut nisl.\r\n\r\nPellentesque eget lobortis erat, non commodo tortor. Vivamus blandit vitae erat sed sollicitudin. Mauris faucibus ut metus vel lobortis. Vivamus lacus ex, tincidunt vitae lectus id, mattis venenatis nibh. Etiam ullamcorper ipsum quis quam consequat tincidunt et eget turpis. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Phasellus condimentum augue ipsum, vel pulvinar elit auctor quis. Phasellus mollis, lacus at vestibulum facilisis, tortor ex posuere ipsum, ut tincidunt arcu quam ac lorem. Nunc sit amet elementum velit. Aliquam condimentum, elit nec dignissim laoreet, diam felis sagittis est, et bibendum ex nisi fermentum nulla. Cras nec tempus dolor. Ut eleifend aliquam nisl, et imperdiet arcu. Sed accumsan felis commodo est venenatis porta. Nunc facilisis mauris tortor, et blandit eros maximus vehicula. Cras hendrerit libero et odio elementum aliquam.",
                post.Id,
                author.Id);

            await context.Comments.AddAsync(comment);

            #endregion

            await context.SaveChangesAsync();
        }
    }
}
