using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace Dars_5ososy_API.Extensions
{
    public static class JwtAuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            services.Configure<JwtSettings>(jwtSettings);

            var key = Convert.FromBase64String(jwtSettings["Key"]!);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                // فزلكة مني بس لو احتاجتها
                options.Events = new JwtBearerEvents
                {
                    // 401 – not authenticated (no token / expired token)
                    OnChallenge = async context =>
                    {
                        context.HandleResponse(); // suppress default response

                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";

                        var response = ApiResponse<object>.Fail(
                            "You are not signed in. Please log in to continue.");

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    },

                    // 403 – authenticated but wrong role
                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";

                        var response = ApiResponse<object>.Fail(
                            "You do not have permission to access this resource.");

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                };
            });

            return services;
        }
    }
}
