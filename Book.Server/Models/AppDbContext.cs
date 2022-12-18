using Book.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Book.Server.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Book.Shared.Models.Book> Books => Set<Book.Shared.Models.Book>();
        public DbSet<User> Users => Set<User>();
    }
}