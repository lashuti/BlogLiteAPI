using Amazon.S3;
using BlogLiteAPI.DataAccess;
using BlogLiteAPI.Infrastructure.Services;
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
            services.AddSingleton<IAmazonS3, AmazonS3Client>();
            services.AddSingleton<IS3ImageService, S3ImageService>();
            services.AddSingleton<ISqsService, SqsService>();
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
