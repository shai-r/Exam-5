using Microsoft.EntityFrameworkCore;
using ozhar_hasfarim.Data;
using ozhar_hasfarim.Enums;
using ozhar_hasfarim.Models;
using ozhar_hasfarim.ViewModels;

namespace ozhar_hasfarim.Service
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBooksSetService _booksSetService;
        private readonly IShelfService _shelfService;
        public BookService(ApplicationDbContext context, IBooksSetService booksSetService, IShelfService shelfService)
        {
            _context = context;
            _booksSetService = booksSetService;
            _shelfService = shelfService;
        }
        public async Task<BookModel> CreateBook(BookVM newBook)
        {
            long shelfId =
                _booksSetService.GetBooksSetByBookSetId(newBook.BooksSetId)
                .Result!.ShelfId;
            if (HowMuchSpace(shelfId) < newBook.Width)
            {
                throw new Exception("There is no room on this shelf.\n" +
                                    "Please try another shelf.\n Thanks");
            }

            if (!IsHeightValidSet(newBook.Height, newBook.BooksSetId))
            {
                throw new Exception("Invalid height in set.\n Thanks");
            }

            if (DistanceBetweenShelfAndBook(newBook.Height, shelfId) < 0)
            {
               throw new Exception("The book is too tall for the shelf. " +
                                    "Please try another page");
            }

            if (!IsValidGenre(newBook.Genre, shelfId))
            {
                throw new Exception("The genre doesn't fit.");
            }

            BookModel model = new()
            {
                Name = newBook.Name,
                Width = newBook.Width,
                Height = newBook.Height,
                Genre = newBook.Genre,
                BooksSetId = newBook.BooksSetId,
                BooksSet = await _booksSetService.GetBooksSetByBookSetId(newBook.BooksSetId)
            };
            _context.Books.Add(model);
            await _context.SaveChangesAsync();
            if (DistanceBetweenShelfAndBook(model.Height, shelfId) >= 10)
            {
                throw new Exception("The book has been successfully added. " +
                    "Please note that the book is more than 10 centimeters lower than the height of the shelf, " +
                    "and it is recommended to delete it from this shelf. " +
                    "Click back to list to continue"
                );
            }
            return model;
        }

        public void DeleteBook(BookModel book)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public List<BookVM>? GetAllBooksBySetId(long booksSetId) =>
            _context.BooksSets
            .Include(booksSet => booksSet.Books)
            .FirstOrDefaultAsync(booksSet => booksSet.Id == booksSetId)
            .Result?.Books
            .Select(book => new BookVM()
            {
                Id = book.Id,
                Name = book.Name,
                Genre = book.Genre,
                Height = book.Height,
                Width = book.Width,
                BooksSetId = booksSetId
            })
            .ToList();

        public async Task<BookModel?> GetBookById(long bookId) =>
            await _context.Books
            .FirstOrDefaultAsync(booksSet => booksSet.Id == bookId);

        public async Task<BookModel?> GetBookByIdWithSetName(long bookId) =>
            await _context.Books
            .Include(book=>book.BooksSet)
            .FirstOrDefaultAsync(booksSet => booksSet.Id == bookId);

        public int HowMuchSpace (long shelfId)
        {
            int wid = _shelfService.GetShelfByID(shelfId).Result!.Width,
                placeTaken = 0;
            if (_shelfService.IsShelfActive(shelfId))
            {
                placeTaken = _booksSetService.GetAllBooksSetsByShlfId(shelfId)!
                .Select(set => _booksSetService.GetWidth(set.Id))
                .Sum();
            }
            return wid - placeTaken;
        }

        public bool IsHeightValidSet(int bookHeight, long bookSetId) =>
            _booksSetService.GetHeight(bookSetId) == 0 ||
            _booksSetService.GetHeight(bookSetId) == bookHeight;
        public int DistanceBetweenShelfAndBook(int height, long shelfId) =>
             _shelfService.GetShelfHeight(shelfId) - height;

        public bool IsValidGenre(GenreEnum genre, long shelfId)=>
            _shelfService.GenreOfLibrary(shelfId) == genre;
    }
}
