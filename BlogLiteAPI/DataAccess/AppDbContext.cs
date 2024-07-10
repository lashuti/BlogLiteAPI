using Microsoft.EntityFrameworkCore;

namespace BlogLiteAPI.DataAccess
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("CURRENT_DATE");
        }
    }
}
