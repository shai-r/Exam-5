using Microsoft.EntityFrameworkCore;
using ozhar_hasfarim.Data;
using ozhar_hasfarim.Enums;
using ozhar_hasfarim.Models;
using ozhar_hasfarim.ViewModels;

namespace ozhar_hasfarim.Service
{
    public class ShelfService : IShelfService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILibraryService _libraryService;
        public ShelfService(ApplicationDbContext context, ILibraryService libraryService)
        {
            _context = context;
            _libraryService = libraryService;
        }

        public List<ShelfVM>? GetAllShelvesByLibrary(long libraryId) =>
             _context.Libraries
            .Include(library => library.Shelves)
            .FirstOrDefaultAsync(library => library.Id == libraryId)
            .Result?.Shelves
            .Select(shelf => new ShelfVM() { Id = shelf.Id, Height = shelf.Height, Width = shelf.Width, LibraryId=libraryId })
            .ToList();


        public async Task<ShelfModel?> GetShelfByID(long id) =>
            await _context.Shelves
            .FirstOrDefaultAsync(shelf => shelf.Id == id);

        public async Task<ShelfModel> CreateShelf(ShelfVM newShelf)
        {
            ShelfModel model = new()
            {
                Height = newShelf.Height,
                Width = newShelf.Width,
                LibraryId = newShelf.LibraryId,
                Library = await _libraryService.GetLibraryByID(newShelf.LibraryId)
            };
            _context.Shelves.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public void DeleteShelf(ShelfModel shelfModel)
        {
            _context.Shelves.Remove(shelfModel);
            _context.SaveChanges();
        }

        public bool IsShelfActive(long shelfId) =>
            _context.Shelves
            .FirstOrDefault(shelf => shelf.Id == shelfId)!
            .BooksSets
            .Any();

        public int GetShelfHeight(long shelfId) =>
            GetShelfByID(shelfId).Result!.Height;

        public GenreEnum GenreOfLibrary(long shelfId) =>
            _context.Shelves
                .Include(s => s.Library)
                .FirstOrDefaultAsync(s => s.Id == shelfId)
                .Result!
                .Library!
                .Genre;
    }
}
