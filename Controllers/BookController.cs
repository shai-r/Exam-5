using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ozhar_hasfarim.Data;
using ozhar_hasfarim.Models;
using ozhar_hasfarim.Service;
using ozhar_hasfarim.ViewModels;

namespace ozhar_hasfarim.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }


        public IActionResult Index(long booksSetId)
        {
            var books = _bookService.GetAllBooksBySetId(booksSetId);
            ViewBag.BooksSetId = booksSetId;
            return View(books);
        }

        // GET: BookModels/Details/5
        public async Task<IActionResult> Details(long id)
        {
            BookModel? bookModel = await _bookService.GetBookById(id);

            if (bookModel == null)
            {
                return NotFound();
            }

            return View(new BookVM()
            {
                Id = bookModel.Id,
                Name = bookModel.Name,
                Genre = bookModel.Genre,
                Height = bookModel.Height,
                Width = bookModel.Width,
                BooksSetId = bookModel.BooksSetId,
            });
        }

        public IActionResult Create(long booksSetId)
        {
            BookVM bookVM = new() { BooksSetId = booksSetId };
            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookVM bookVM)
        {
            try
            {
                var model = await _bookService.CreateBook(bookVM);
                bookVM.Id = model.Id;
                return View("Details", bookVM);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(bookVM);
            }
        }

        public async Task<IActionResult> Delete(long id)
        {
            BookModel? bookModel = await _bookService.GetBookByIdWithSetName(id);

            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id, long booksSetId)
        {
            BookModel? bookModel = await _bookService.GetBookByIdWithSetName(id);
            if (bookModel != null)
            {
                _bookService.DeleteBook(bookModel);
            }
            return RedirectToAction("Index", new { booksSetId });
        }
    }
}
