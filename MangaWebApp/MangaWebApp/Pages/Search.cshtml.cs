using System;
using System.Collections.Generic;
using System.Linq;
using MangaWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MangaWebApp.Pages
{
    public class SearchModel : PageModel
    {

        public IEnumerable<Manga> Mangas = Enumerable.Empty<Manga>();
        MangaService mangaService;
        public SearchParams SearchParams = new SearchParams();
        public SearchModel(MangaService mangaService) {

            this.mangaService = mangaService;
        }

        public async void OnGetAsync(int Year, int Price, string Genre, int pageIndex)
        {
            if (SearchParams.PageIndex != pageIndex) {
                SearchParams.PageIndex = pageIndex;
                Mangas = mangaService.Search(SearchParams);
                return;
            }
            
            SearchParams.Year = Year;
            SearchParams.Genre = Genre;
            SearchParams.Price = Price;
            Mangas = mangaService.Search(SearchParams);

        }
    }
}