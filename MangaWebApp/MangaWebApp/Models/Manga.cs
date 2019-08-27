using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MangaWebApp.Models
{
    public class Manga
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Titre")]
        [Required(ErrorMessage = "Le titre est obligatoire.")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "La description est obligatoire.")]
        public string Description { get; set; }
        [Display(Name = "Année")]
        [Required(ErrorMessage = "L'année est obligatoire.")]
        public int Year { get; set; }
        [Display(Name = "Prix")]
        [Required(ErrorMessage = "Le prix est obligatoire.")]
        public decimal Price { get; set; }
        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Le genre est obligatoire.")]
        public string Genre { get; set; }
        public int AverageRating { get; set; }
    }
}
