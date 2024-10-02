using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog_DevIO.Identity
{
    public static class RegisterIdentityServices
    {
        public static IServiceCollection AddIdentityContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<IdentityDBContext>(options =>
           options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                        .AddRoles<IdentityRole>()
                        .AddEntityFrameworkStores<IdentityDBContext>();

            return services;
        }
    }
}
