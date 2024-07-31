using Microsoft.AspNetCore.Mvc;
using ozhar_hasfarim.Service;

namespace ozhar_hasfarim.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        public async Task<IActionResult> Index() =>
            View(await _libraryService.GetAllLibrary());

    }
}
