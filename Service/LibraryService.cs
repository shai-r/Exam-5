using Microsoft.EntityFrameworkCore;
using ozhar_hasfarim.Data;
using ozhar_hasfarim.Models;

namespace ozhar_hasfarim.Service
{
    public class LibraryService : ILibraryService
    {
        private readonly ApplicationDbContext _context;
        public LibraryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LibraryModel>> GetAllLibrary() => await _context.Libraries
            .Include(library => library.Shelves)
            .ThenInclude(shelf => shelf.BooksSets)
            .ThenInclude(booksset => booksset.Books)
            .ToListAsync();
    }
}
