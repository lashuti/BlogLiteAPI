using BlogLiteAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlogLiteAPI.Infrastructure.Configuration
{
    public static class ServiceConfiguration
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            AddAWS(builder.Services);
            AddDbContext(builder.Services);

            return builder;
        }

        public static void AddAWS(IServiceCollection services)
        {

        }

        public static void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(
                cfg =>
                {
                    cfg.UseNpgsql("Server=BlogLiteAPI;Port=5432;Database=BlogLite;User Id=postgres;Password=postgres;Include Error Detail=true");
                });

        }
    }
}
