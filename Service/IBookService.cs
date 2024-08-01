using ozhar_hasfarim.Models;
using ozhar_hasfarim.ViewModels;

namespace ozhar_hasfarim.Service
{
    public interface IBookService
    {
        List<BookVM>? GetAllBooksBySetId(long booksSetId);

        Task<BookModel?> GetBookById(long bookId);

        Task<BookModel?> GetBookByIdWithSetName(long bookId);

        Task<BookModel> CreateBook(BookVM newBook);

        void DeleteBook(BookModel book);
    }
}
