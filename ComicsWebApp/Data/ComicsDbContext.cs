using ComicsWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicsWebApp.Data
{
    public class ComicsDbContext: DbContext
    {
        public DbSet<Comics> Comics { get; set; }
        public DbSet<ComicsGenre> ComicsGenres { get; set; }
        public DbSet<ComicsPages> ComicsPages { get; set; }

        public ComicsDbContext(DbContextOptions<ComicsDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comics>()
                .Property(c => c.Language)
                .HasConversion<string>();

            modelBuilder.Entity<Comics>()
                .Property(c => c.AvailabilityStatus)
                .HasConversion<string>();

            modelBuilder.Entity<Comics>()
                .Property(c => c.CoverType)
                .HasConversion<string>();

            modelBuilder.Entity<Comics>()
                .Property(c => c.PagesNumber)
                .HasMaxLength(4);

            modelBuilder.Entity<Comics>()
                .Property(c => c.YearOfPublication)
                .HasMaxLength(4);

            modelBuilder.Entity<Comics>()
                .Property(c => c.Price)
                .HasMaxLength(8);

            modelBuilder.Entity<Comics>()
                .Property(c => c.Name)
                .HasMaxLength(200);

            modelBuilder.Entity<Comics>()
                .Property(c => c.Author)
                .HasMaxLength(100);

            modelBuilder.Entity<Comics>()
                .Property(c => c.Publisher)
                .HasMaxLength(70);

            modelBuilder.Entity<Comics>()
                .Property(c => c.PublicationFormat)
                .HasMaxLength(15);

            modelBuilder.Entity<ComicsGenre>().HasData(
                new ComicsGenre[]
                {
                    new ComicsGenre {Id = 1, GenreName = "Detective"},
                    new ComicsGenre {Id = 2, GenreName = "Historical"},
                    new ComicsGenre {Id = 3, GenreName = "Science Fiction"},
                    new ComicsGenre {Id = 4, GenreName = "Educational"},
                    new ComicsGenre {Id = 5, GenreName = "Adventure"},
                    new ComicsGenre {Id = 6, GenreName = "Romantic"},
                    new ComicsGenre {Id = 7, GenreName = "Horror"},
                    new ComicsGenre {Id = 8, GenreName = "Fantasy"},
                    new ComicsGenre {Id = 9, GenreName = "Humor"}
                });
        }
    }
}
