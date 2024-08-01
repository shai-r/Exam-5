using System.ComponentModel.DataAnnotations;

namespace ozhar_hasfarim.ViewModels
{
    public class ShelfVM
    {
        public long Id { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Width { get; set; }

        public long LibraryId { get; set; }
    }
}
