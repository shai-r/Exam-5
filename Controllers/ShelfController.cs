using Microsoft.AspNetCore.Mvc;
using ozhar_hasfarim.Models;
using ozhar_hasfarim.Service;
using ozhar_hasfarim.ViewModels;

namespace ozhar_hasfarim.Controllers
{
    public class ShelfController : Controller
    {
        private long _LibraryId;
        private readonly IShelfService _shelfService;
        public ShelfController(IShelfService shelfService)
        {
            _shelfService = shelfService;
        }
        public IActionResult Index(long libraryId)
        {
            var shelfVMs = _shelfService.GetAllShelvesByLibrary(libraryId);
            ViewBag.LibraryId = libraryId;
            return View(shelfVMs);
        }

        public async Task<IActionResult> Details(long id, long libraryId)
        {
            ShelfModel? shelf = await _shelfService.GetShelfByID(id);

            return (shelf == null) ?
                RedirectToAction("Index", new { id = libraryId }) :
                View(new ShelfVM()
                {
                    Id = shelf.Id,
                    Height = shelf.Height,
                    Width = shelf.Width,
                    LibraryId = shelf.LibraryId
                });
        }

        public IActionResult Create(long libraryId) 
        {
            ShelfVM shelfVM = new () { LibraryId = libraryId };
            return View(shelfVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShelfVM shelf)
        {
            var model = await _shelfService.CreateShelf(shelf); ;
            shelf.Id = model.Id;
            return View("Details", shelf);
        }

        public async Task<IActionResult> Delete(long id)
        {
            ShelfModel? shelf = await _shelfService.GetShelfByID(id);
            if (shelf == null)
            {
                return NotFound();
            }
            return View(shelf);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id, long libraryId)
        {
            ShelfModel? shelf = await _shelfService.GetShelfByID(id);
            if (shelf != null)
            {
                _shelfService.DeleteShelf(shelf);
            }
            return RedirectToAction("Index", new { libraryId = libraryId });
        }
    }
}
