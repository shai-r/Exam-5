using ozhar_hasfarim.Enums;
using System.ComponentModel.DataAnnotations;

namespace ozhar_hasfarim.Models
{
    public class LibraryModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public required GenreEnum Genre { get; set; }

        public IEnumerable<ShelfModel> Shelves { get; set; } = [];
    }
}
