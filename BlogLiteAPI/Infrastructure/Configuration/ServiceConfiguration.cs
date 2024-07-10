using BlogLiteAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

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
                    cfg.UseNpgsql("Database=BlogLiteDB;Port=5432;Host=bloglite.c5kqu82aoms2.eu-central-1.rds.amazonaws.com;Username=postgres;Password=postgres;");
                });

        }
    }
}
