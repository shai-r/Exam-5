using ozhar_hasfarim.Models;
using ozhar_hasfarim.ViewModels;

namespace ozhar_hasfarim.Service
{
    public class BookService : IBookService
    {
        public Task<BooksSetModel> CreateBook(BookVM newBook)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(BookModel book)
        {
            throw new NotImplementedException();
        }

        public List<BookVM>? GetAllBooksBySetId(long booksSetId)
        {
            throw new NotImplementedException();
        }

        public Task<BooksSetModel?> GetBookById(long bookId)
        {
            throw new NotImplementedException();
        }
    }
}
