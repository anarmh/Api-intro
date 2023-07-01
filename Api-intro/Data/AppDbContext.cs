
using Api_intro.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_intro.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
