using ozhar_hasfarim.Models;
using ozhar_hasfarim.ViewModels;

namespace ozhar_hasfarim.Service
{
    public interface ILibraryService
    {
        Task<IEnumerable<LibraryVM>> GetAllLibrary();

        Task<LibraryModel?> GetLibraryByID(long id);

        Task<LibraryModel> CreateLibrary(LibraryVM newLibrary);

        void DeleteLibrary(LibraryModel library);
    }
}
