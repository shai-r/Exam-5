using ozhar_hasfarim.Models;

namespace ozhar_hasfarim.Service
{
    public interface ILibraryService
    {
        Task<IEnumerable<LibraryModel>> GetAllLibrary();
    }
}
