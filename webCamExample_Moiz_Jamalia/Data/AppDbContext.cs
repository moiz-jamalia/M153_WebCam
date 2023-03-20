using Microsoft.EntityFrameworkCore;
using webCamExample_Moiz_Jamalia.Models;

namespace webCamExample_Moiz_Jamalia.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ImageStore> ImageStores { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder Builder)
        {
            base.OnModelCreating(Builder);
        }
    }
}
