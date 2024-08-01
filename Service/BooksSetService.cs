using Microsoft.EntityFrameworkCore;
using ozhar_hasfarim.Data;
using ozhar_hasfarim.Models;
using ozhar_hasfarim.ViewModels;

namespace ozhar_hasfarim.Service
{
    public class BooksSetService : IBooksSetService
    {
        private readonly ApplicationDbContext _context;
        private readonly IShelfService _shelfService;
        public BooksSetService(ApplicationDbContext context, IShelfService shelfService)
        {
            _context = context;
            _shelfService = shelfService;
        }

        public async Task<BooksSetModel> CreateBookSet(BooksSetVM newBooksSet)
        {
            BooksSetModel model = new()
            {
                Id = newBooksSet.Id,
                Name = newBooksSet.Name,
                ShelfId = newBooksSet.ShelfId,
                Shelf = await _shelfService.GetShelfByID(newBooksSet.ShelfId)
            };
            _context.BooksSets.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public void DeleteBookSet(BooksSetModel booksSet)
        {
            _context.BooksSets.Remove(booksSet);
            _context.SaveChanges();
        }

        public List<BooksSetVM>? GetAllBooksSetsByShlfId(long shlfId) =>
              _context.Shelves
            .Include(shelf => shelf.BooksSets)
            //.ThenInclude(set=>set.Books)
            .FirstOrDefaultAsync(shelf => shelf.Id == shlfId)
            .Result?.BooksSets
            .Select(booksSet => new BooksSetVM() 
            { 
                Id = booksSet.Id, 
                Name = booksSet.Name, 
                ShelfId = shlfId 
            })
            .ToList();

        public async Task<BooksSetModel?> GetBooksSetByBookSetId(long booksSetId) =>
            await _context.BooksSets
            .FirstOrDefaultAsync(booksSet => booksSet.Id == booksSetId);

        public int GetWidth(long booksSetId)
        {
            return GetBooksSetByBookSetId(booksSetId).Result
                 .Books
                 .Select(book => book.Width)
                 .Sum();
        }
    }
}
