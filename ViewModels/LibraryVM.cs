﻿using Microsoft.EntityFrameworkCore;
using ozhar_hasfarim.Enums;
using System.ComponentModel.DataAnnotations;

namespace ozhar_hasfarim.ViewModels
{
    public class LibraryVM
    {
        public long Id { get; set; }

    
        public GenreEnum Genre { get; set; }
    }
}
