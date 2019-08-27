using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MangaWebApp.Models
{
    public class Rating
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Note")]
        [Required(ErrorMessage = "La note est obligatoire.")]
        [Range(0, 5, ErrorMessage = "Veuillez mettre une note de 0 à 5")]
        public int Value { get; set; }

        // Foreign Key
        [HiddenInput(DisplayValue = false)]
        [Required]
        public int MangaId { get; set; }
        [HiddenInput(DisplayValue = false)]
        [Required]
        public int UserId { get; set; }
        // Navigation property
        public Manga Manga { get; set; }

        public User User { get; set; }
    }
}
