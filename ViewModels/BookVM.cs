using ozhar_hasfarim.Enums;
using System.ComponentModel.DataAnnotations;

namespace ozhar_hasfarim.ViewModels
{
    public class BookVM
    {
        public long Id { get; set; }

        [Required, StringLength(32, MinimumLength = 4,ErrorMessage = "Name should be in a range of 4 - 32")]
        public string Name { get; set; }

        [Required]
        public GenreEnum Genre { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Width { get; set; }

        public long BooksSetId {  get; set; }
    }
}