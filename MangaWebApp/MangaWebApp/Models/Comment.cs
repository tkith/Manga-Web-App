using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MangaWebApp.Models
{
    public class Comment
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Text")]
        [Required(ErrorMessage = "Le texte est obligatoire.")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

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
