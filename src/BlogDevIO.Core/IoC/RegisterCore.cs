﻿using Blog_DevIO.Core.Data.Abstractions;
using Blog_DevIO.Core.Data;
using Blog_DevIO.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Blog_DevIO.Core.Services;
using Blog_DevIO.Core.Services.Abstractions;

namespace Blog_DevIO.Core.IoC
{
    public static class RegisterCore
    {
        public static WebApplicationBuilder AddEF(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<BlogContext>(options =>
              options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                 .AddRoles<IdentityRole>()
                 .AddEntityFrameworkStores<BlogContext>()
                  .AddSignInManager()
                  .AddRoleManager<RoleManager<IdentityRole>>()
                  .AddDefaultTokenProviders();


            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();


            return builder;
        }

        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICommentService, CommentService>();

            return builder;
        }
    }
}