using ozhar_hasfarim.Enums;
using ozhar_hasfarim.Models;
using ozhar_hasfarim.ViewModels;

namespace ozhar_hasfarim.Service
{
    public interface IShelfService
    {
        List<ShelfVM>? GetAllShelvesByLibrary(long libraryId);

        Task<ShelfModel?> GetShelfByID(long id);

        Task<ShelfModel> CreateShelf(ShelfVM newShelf);

        void DeleteShelf(ShelfModel shelfModel);

        bool IsShelfActive(long shelfId);

        int GetShelfHeight(long shelfId);

        GenreEnum GenreOfLibrary(long shelfId);
    }
}
