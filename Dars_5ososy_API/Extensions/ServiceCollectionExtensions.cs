using Dars_5ososy_API.Application.Mappings;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Infrastructure.Repositories;

namespace Dars_5ososy_API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IGenericRepository<>));

            services.AddScoped<UserRepository>();
            services.AddScoped<SubjectRepository>();
            services.AddScoped<EducationSystemRepository>();
            services.AddScoped<EducationStageRepository>();
            services.AddScoped<ReviewRepository>();
            services.AddScoped<FavoriteRepository>();
            services.AddScoped<ProvinceRepository>();
            services.AddScoped<GovernorateRepository>();
            services.AddScoped<AreaRepository>();

            services.AddScoped<TokenService>();
            services.AddScoped<EmailService>();
            services.AddScoped<AuthService>();

            services.AddScoped<UserService>();
            services.AddScoped<SubjectService>();
            services.AddScoped<EducationSystemService>();
            services.AddScoped<EducationStageService>();
            services.AddScoped<ReviewService>();
            services.AddScoped<FavoriteService>();
            services.AddScoped<ProvinceService>();
            services.AddScoped<GovernorateService>();
            services.AddScoped<AreaService>();

            services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            return services;
        }
    }
}