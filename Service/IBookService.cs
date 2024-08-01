using ozhar_hasfarim.Models;
using ozhar_hasfarim.ViewModels;

namespace ozhar_hasfarim.Service
{
    public interface IBookService
    {
        List<BookVM>? GetAllBooksBySetId(long booksSetId);

        Task<BooksSetModel?> GetBookById(long bookId);

        Task<BooksSetModel> CreateBook(BookVM newBook);

        void DeleteBook(BookModel book);
    }
}
