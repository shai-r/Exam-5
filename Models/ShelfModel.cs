using System.ComponentModel.DataAnnotations;

namespace ozhar_hasfarim.Models
{
    public class ShelfModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public required int Height { get; set; }

        [Required]
        public required int Width { get; set; }

        public LibraryModel? Library { get; set; }
        public long LibraryId { get; set; }

        public List<BooksSetModel> BooksSets { get; set; } = [];
    }
}
