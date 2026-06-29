using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Domain.Entities;

namespace Dars_5ososy_API.Extensions
{
    public static class EmailServiceExtensions
    {
        public static IServiceCollection AddEmailService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<EmailSettings>(
                configuration.GetSection("EmailSettings"));

            services.AddScoped<EmailService>();

            return services;
        }
    }
}
