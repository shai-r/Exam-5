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
            var model = await _bookService.CreateBook(bookVM);
            bookVM.Id = model.Id;
            return View("Details", bookVM);
        }

         // GET: BookModels/Edit/5
         /*public async Task<IActionResult> Edit(long? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var bookModel = await _context.Books.FindAsync(id);
             if (bookModel == null)
             {
                 return NotFound();
             }
             ViewData["BooksSetId"] = new SelectList(_context.BooksSets, "Id", "Name", bookModel.BooksSetId);
             return View(bookModel);
         }

         // POST: BookModels/Edit/5
         // To protect from overposting attacks, enable the specific properties you want to bind to.
         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Genre,Height,Width,BooksSetId")] BookModel bookModel)
         {
             if (id != bookModel.Id)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(bookModel);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!BookModelExists(bookModel.Id))
                     {
                         return NotFound();
                     }
                     else
                     {
                         throw;
                     }
                 }
                 return RedirectToAction(nameof(Index));
             }
             ViewData["BooksSetId"] = new SelectList(_context.BooksSets, "Id", "Name", bookModel.BooksSetId);
             return View(bookModel);
         }*/

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

        /*private bool BookModelExists(long id)
        {
            return _context.Books.Any(e => e.Id == id);
        }*/
    }
}
