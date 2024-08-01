using Microsoft.AspNetCore.Mvc;
using ozhar_hasfarim.Models;
using ozhar_hasfarim.Service;
using ozhar_hasfarim.ViewModels;

namespace ozhar_hasfarim.Controllers
{
    public class BooksSetController : Controller
    {
        private readonly IBooksSetService _booksSetService;
        public BooksSetController(IBooksSetService booksSetService)
        {
            _booksSetService = booksSetService;
        }
        public IActionResult Index(long shelfId)
        {
            var booksSetVM = _booksSetService.GetAllBooksSetsByShlfId(shelfId);
            ViewBag.ShelfId = shelfId;
            return View(booksSetVM);
        }

        public async Task<IActionResult> Details(long id, long shelfId)
        {
            BooksSetModel? booksSet = await _booksSetService.GetBooksSetByBookSetId(id);

            return (booksSet == null) ?
                RedirectToAction("Index", shelfId) :
                View(new BooksSetVM()
                {
                    Id = booksSet.Id,
                    Name = booksSet.Name,
                    ShelfId = shelfId
                });
        }

        public IActionResult Create(long shelfId)
        {
            BooksSetVM booksSetVM = new() { ShelfId = shelfId };
            return View(booksSetVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BooksSetVM booksSetVM)
        {
            var model = await _booksSetService.CreateBookSet(booksSetVM); ;
            booksSetVM.Id = model.Id;
            return View("Details", booksSetVM);
        }

        public async Task<IActionResult> Delete(long id)
        {
            BooksSetModel? booksSet = await _booksSetService.GetBooksSetByBookSetId(id);
            if (booksSet == null)
            {
                return NotFound();
            }
            return View(booksSet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id, long shelfId)
        {
            BooksSetModel? booksSet = await _booksSetService.GetBooksSetByBookSetId(id);
            if (booksSet != null)
            {
                _booksSetService.DeleteBookSet(booksSet);
            }
            return RedirectToAction("Index", new { shelfId });
        }
    }
}