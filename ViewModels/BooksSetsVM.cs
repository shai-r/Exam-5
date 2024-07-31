using System.ComponentModel.DataAnnotations;

namespace ozhar_hasfarim.ViewModels
{
    public class BooksSetVM
    {
        public long Id { get; set; }

        [Required, StringLength(32, MinimumLength = 4, ErrorMessage = "Name should be in a range of 4 - 32")]
        public required string Name { get; set; }
    }
}