using Asp.Versioning;
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

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAddressRepository, UserAddressRepository>();
            services.AddScoped<IAvailabilitySlotRepository, AvailabilitySlotRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ITeacherSubjectRepository, TeacherSubjectRepository>();
            services.AddScoped<IEducationSystemRepository, EducationSystemRepository>();
            services.AddScoped<IEducationStageRepository, EducationStageRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IGovernorateRepository, GovernorateRepository>();
            services.AddScoped<IAreaRepository, AreaRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();

            services.AddScoped<AuthService>();
            services.AddScoped<UserService>();
            services.AddScoped<UserAddressService>();
            services.AddScoped<AvailabilitySlotService>();
            services.AddScoped<SubjectService>();
            services.AddScoped<TeacherSubjectService>();
            services.AddScoped<EducationSystemService>();
            services.AddScoped<EducationStageService>();
            services.AddScoped<ReviewService>();
            services.AddScoped<FavoriteService>();
            services.AddScoped<BookingService>();
            services.AddScoped<ProvinceService>();
            services.AddScoped<GovernorateService>();
            services.AddScoped<AreaService>();
            services.AddScoped<IImageService, ImageService>();

            services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            // Configure API Versioning
            services.AddApiVersioning(options =>
            {
                // Default version to use when none is specified (e.g., 1.0)
                options.DefaultApiVersion = new ApiVersion(1, 0);

                // Assume DefaultApiVersion when the client doesn't provide one
                options.AssumeDefaultVersionWhenUnspecified = true;

                // Report supported and deprecated versions in the response headers (api-supported-versions, api-deprecated-versions)
                options.ReportApiVersions = true;

                // Combine multiple ways to read the version (Query string, Header, and Media Type)
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("ver")
                );
            })
            .AddApiExplorer(options =>
            {
                // Format the version group name as 'v' + major + minor (e.g., v1.0)
                options.GroupNameFormat = "'v'VVV";

                // Substitute the version value in the route template (required for URL segment versioning)
                options.SubstituteApiVersionInUrl = true;
            });

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