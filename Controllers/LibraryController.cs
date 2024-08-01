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

        [HttpPost]
        public async Task<IActionResult> Create(LibraryVM libraryVM)
        {
            var model = await _libraryService.CreateLibrary(libraryVM);
            libraryVM.Id = model.Id;
            return View("Details", libraryVM);
        }

        public async Task<IActionResult> Delete(long id)
        {
            LibraryModel? library = await _libraryService.GetLibraryByID(id);
            if (library == null)
            {
                return NotFound();
            }

            return View(library);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            LibraryModel? library = await _libraryService.GetLibraryByID(id);
            if (library != null)
            {
                _libraryService.DeleteLibrary(library);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
