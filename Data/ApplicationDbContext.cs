using Microsoft.EntityFrameworkCore;
using ozhar_hasfarim.Models;
namespace ozhar_hasfarim.Data
{
	public class ApplicationDbContext : DbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<BookModel> Books { get; set; }
		public DbSet<BooksSetModel> BooksSets { get; set; }
		public DbSet<ShelfModel> Shelves { get; set; }
		public DbSet<LibraryModel> Libaries { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<LibraryModel>()
				.HasMany(libary => libary.Shelves)
				.WithOne(Shelf => Shelf.Library)
				.HasForeignKey(shelf => shelf.LibraryId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ShelfModel>()
				.HasMany(shelf => shelf.BooksSets)
				.WithOne(booksSet => booksSet.Shelf)
				.HasForeignKey(BooksSet => BooksSet.ShelfId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<BooksSetModel>()
				.HasMany(booksSet => booksSet.Books)
				.WithOne(book => book.BooksSet)
				.HasForeignKey(book => book.BooksSetId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
