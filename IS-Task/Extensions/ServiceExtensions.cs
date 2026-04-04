using IS_Task.Data;
using Microsoft.EntityFrameworkCore;

namespace IS_Task.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention());

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
    }
}
