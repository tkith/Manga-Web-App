using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangaWebApp.Models
{
    public class MangaView
    {
        public Manga Manga { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public User CurrentUser { get; set; }
        public IEnumerable<Manga> Mangas { get; internal set; }
        public Rating Mark { get; internal set; }
    }
}
