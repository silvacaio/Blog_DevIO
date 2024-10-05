using Blog_DevIO.Application.Services;
using Blog_DevIO.Application.Services.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Blog_DevIO.Application.IoC
{
    public static class RegisterApplicationServices
    {
        public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPostService, PostService>();    

            return builder;
        }
    }
}
