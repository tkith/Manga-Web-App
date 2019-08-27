using Microsoft.EntityFrameworkCore;
using MangaAPI.Models;
namespace MangaAPI.Data
{
    public class MangaAPIContext : DbContext
    {
        public MangaAPIContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
