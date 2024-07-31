using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ozhar_hasfarim.Models;
using ozhar_hasfarim.Service;
using ozhar_hasfarim.ViewModels;

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

        public async Task<IActionResult> Details(long id)
        {
            LibraryModel? library = await _libraryService.GetLibraryByID(id);

            return (library == null) ?
                RedirectToAction("Index") :
                View(new LibraryVM()
                {
                    Id = library.Id,
                    Genre = library.Genre,
                });
        }

        public IActionResult Create() => View();

        /*[HttpPost]
        [ValidateAntiForgeryToken]*/
        /*public IActionResult Create(LibraryVM libraryVM)
        {
            if (!ModelState.IsValid) { return BadRequest(""); }

            _libraryService.CreateLibrary(libraryVM);
            return View("Details", libraryVM);
        }*/
    }
}
