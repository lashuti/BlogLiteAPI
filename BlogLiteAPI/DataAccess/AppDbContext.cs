using Microsoft.EntityFrameworkCore;

namespace BlogLiteAPI.DataAccess
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Blog> Blogs { get; set; }
    }
}
