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
                Name = newBooksSet.Name,
                ShelfId = newBooksSet.ShelfId,
                Shelf = await _shelfService.GetShelfByID(newBooksSet.ShelfId)
            };
            _context.BooksSets.Add(model);
            //_context.Shelves.f
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
            int width = 0;
            bool a = IsSetActive(booksSetId);
            if (a)
            {
                var t = GetBooksSetByBookSetId(booksSetId).Result!;
                var tt = t.Books;
                var ttt = tt.Select(book => book.Width);
                width = ttt.Sum();
            }
            return width;
        }

        public int GetHeight(long booksSetId) =>
            IsSetActive(booksSetId) ?
            GetBooksSetByBookSetId(booksSetId).Result!.Books
            .FirstOrDefault()!
            .Height :
            0;

        public bool IsSetActive(long booksSetId)
        {
            var t = _context.Books;
            var tt = t.FirstOrDefault(book => book.BooksSetId == booksSetId)!;
            return tt != null;
        }
    }
}
