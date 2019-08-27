using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MangaWebApp.Models
{
    public class User
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Nom d'utilisateur")]
        [Required(ErrorMessage = "Le nom d'utilisateur est obligatoire.")]
        public string Name { get; set; }
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "L'e-mail n'est pas valide.")]
        [Required(ErrorMessage = "L'e-mail est obligatoire.")]
        public string Email { get; set; }
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        public string Password { get; set; }
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        public string ConfirmPassword { get; set; }
    }
}
