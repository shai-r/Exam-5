﻿using ozhar_hasfarim.Enums;
using System.ComponentModel.DataAnnotations;

namespace ozhar_hasfarim.Models
{
    public class BookModel
    {
        [Key]
        public long Id { get; set; }

        [Required, StringLength(32, MinimumLength =4)] 
        public string Name { get; set; }

        [Required]
        public required GenreEnum Genre { get; set; } 

        [Required]
        public required int Height { get; set; }

        [Required]
        public required int Width { get; set; }

        public BooksSetModel? BooksSet { get; set; }

        public long BooksSetId { get; set; }
    }
}