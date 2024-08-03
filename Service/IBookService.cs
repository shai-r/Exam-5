using ozhar_hasfarim.Enums;
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

        int HowMuchSpace (long shelfId);

        bool IsHeightValidSet(int height, long bookSetId);

        int DistanceBetweenShelfAndBook(int height, long bookSetId);

        bool IsValidGenre(GenreEnum genre, long shelfId);
    }
}
