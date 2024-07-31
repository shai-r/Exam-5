using System.ComponentModel.DataAnnotations;

namespace ozhar_hasfarim.ViewModels
{
    public class ShelfVM
    {
        public long Id { get; set; }

        [Required]
        public required int Height { get; set; }

        [Required]
        public required int Width { get; set; }
    }
}
