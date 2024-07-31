using Microsoft.EntityFrameworkCore;
using ozhar_hasfarim.Enums;
using System.ComponentModel.DataAnnotations;

namespace ozhar_hasfarim.Models
{
    [Index(nameof(Genre), IsUnique = true)]
    public class LibraryModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public required GenreEnum Genre { get; set; }

        public List<ShelfModel> Shelves { get; set; } = [];
    }
}
