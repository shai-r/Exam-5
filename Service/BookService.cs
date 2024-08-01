using Microsoft.EntityFrameworkCore;
using ozhar_hasfarim.Data;
using ozhar_hasfarim.Models;
using ozhar_hasfarim.ViewModels;

namespace ozhar_hasfarim.Service
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBooksSetService _booksSetService;
        public BookService(ApplicationDbContext context, IBooksSetService booksSetService)
        {
            _context = context;
            _booksSetService = booksSetService;
        }
        public async Task<BookModel> CreateBook(BookVM newBook)
        {
            BookModel model = new()
            {
                Id = newBook.Id,
                Name = newBook.Name,
                Width = newBook.Width,
                Height = newBook.Height,
                Genre = newBook.Genre,
                BooksSetId = newBook.BooksSetId,
                BooksSet = await _booksSetService.GetBooksSetByBookSetId(newBook.BooksSetId)
            };
            _context.Books.Add(model);
            await _context.SaveChangesAsync();
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
    }
}
