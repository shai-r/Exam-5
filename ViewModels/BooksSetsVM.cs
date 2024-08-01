using System.ComponentModel.DataAnnotations;

namespace ozhar_hasfarim.ViewModels
{
    public class BooksSetVM
    {
        public long Id { get; set; }

        [Required, StringLength(32, MinimumLength = 4, ErrorMessage = "Name should be in a range of 4 - 32")]
        public string Name { get; set; }

        public long ShelfId { get; set; }
    }
}