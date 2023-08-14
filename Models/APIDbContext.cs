using Microsoft.EntityFrameworkCore;

namespace MovieAPI.Models
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions option):base(option) { }

        public DbSet<Movie> Movies { get; set; }
    }

}
