using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Blog_DevIO.API.Configurations
{
    public static class IdentityConfig
    {
        public static WebApplicationBuilder AddJwt(this WebApplicationBuilder builder)
        {
            var appSettingsSection = builder.Configuration.GetSection("AppSettings");
            builder.Services.Configure<JwtSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = appSettings.Audience,
                        ValidIssuer = appSettings.Issuer
                    };
                });

            return builder;
        }
    }
}