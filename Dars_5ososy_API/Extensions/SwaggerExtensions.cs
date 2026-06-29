using Microsoft.OpenApi.Models;

namespace Dars_5ososy_API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "Dars 5ososy API", 
                    Version = "v1",
                    Description = "A production-ready RESTful API that connects students with private teachers. Teachers can manage profiles, teaching schedules, and lesson bookings, while students can discover teachers, reserve sessions, leave reviews, and track their learning activities.",
                    Contact = new OpenApiContact
                    {
                        Name = "Mohammed Atef",
                        Email = "mohammed.atef.abdelkader@gmail.com",
                        Url = new Uri("https://mohammedatefportfolio.vercel.app/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by your token"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }
    }
}
