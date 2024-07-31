using ozhar_hasfarim.Enums;
using System.ComponentModel.DataAnnotations;

namespace ozhar_hasfarim.ViewModels
{
    public class BookVm
    {
        public long Id { get; set; }

        [Required, StringLength(32, MinimumLength = 4,ErrorMessage = "Name should be in a range of 4 - 32")]
        public string Name { get; set; }

        [Required]
        public required GenreEnum Genre { get; set; }

        [Required]
        public required int Height { get; set; }

        [Required]
        public required int Width { get; set; }
    }
}