using ozhar_hasfarim.Models;
using ozhar_hasfarim.ViewModels;

namespace ozhar_hasfarim.Service
{
    public interface IBooksSetService
    {
        List<BooksSetVM>? GetAllBooksSetsByShlfId(long shlfId);

        Task<BooksSetModel?> GetBooksSetByBookSetId(long booksSetId);

        Task<BooksSetModel> CreateBookSet(BooksSetVM newBooksSet);

        void DeleteBookSet(BooksSetModel booksSet);

        int GetWidth(long booksSetId);

    }
}
