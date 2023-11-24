using Microsoft.EntityFrameworkCore;

namespace CRUD_Library.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<BookReader> BookReaders { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
