using System.ComponentModel.DataAnnotations;

namespace ozhar_hasfarim.Models
{
    public class BooksSetModel
    {
        [Key]
        public long Id { get; set; }

        [Required, StringLength(32,MinimumLength = 4)]
        public required string Name { get; set; }

        public ShelfModel? Shelf { get; set; }

        public long ShelfId {  get; set; } 

        public IEnumerable<BookModel> Books { get; set; } = [];
    }
}