using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog_DevIO.Data
{
    public static class RegisterEFServices
    {
        public static IServiceCollection AddEF(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<BlogContext>(options =>
             options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
