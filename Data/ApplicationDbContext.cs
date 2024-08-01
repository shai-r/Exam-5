using Microsoft.EntityFrameworkCore;
using ozhar_hasfarim.Enums;
using ozhar_hasfarim.Models;
using ozhar_hasfarim.ViewModels;

namespace ozhar_hasfarim.Data
{
	public class ApplicationDbContext : DbContext
	{

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			Database.EnsureCreated();
            Seed();
        }

		private void Seed()
		{
			if (!Libraries.Any())
			{
				List<LibraryModel> libraries = [
					new()
					{
						Genre=GenreEnum.Musar,
						Shelves = [
							new(){
								Height=20,
								Width=40,
								BooksSets=[
									new(){
										Name="ספרי הרמח\"ל",
										Books=[
											new(){
												Name="דרך ה'",
												Genre = GenreEnum.Musar,
												Height=19,
												Width=3
											},
                                            new(){
                                                Name="מסילת ישרים",
                                                Genre = GenreEnum.Musar,
                                                Height=19,
                                                Width=7
                                            }
                                        ]
									},
									new(){
                                        Name="אורחות צדיקים המבואר",
                                        Books=[
                                            new(){
                                                Name="אורחות צדיקים המבואר כרך א'",
                                                Genre = GenreEnum.Musar,
                                                Height=15,
                                                Width=7
                                            },
                                            new(){
                                                Name="אורחות צדיקים המבואר כרך ב",
                                                Genre = GenreEnum.Musar,
                                                Height=15,
                                                Width=9
                                            },
                                            new(){
                                                Name="אורחות צדיקים המבואר כרך ג",
                                                Genre = GenreEnum.Musar,
                                                Height=15,
                                                Width=5
                                            }
                                        ]
                                    }
								]
							}
						]
					}
				];
				Libraries.AddRange( libraries );
				SaveChanges();
			}
		}
		public DbSet<BookModel> Books { get; set; }
		public DbSet<BooksSetModel> BooksSets { get; set; }
		public DbSet<ShelfModel> Shelves { get; set; }
		public DbSet<LibraryModel> Libraries { get; set; }

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
	    public DbSet<ozhar_hasfarim.ViewModels.BookVM> BookVM { get; set; } = default!;
	}
}
