using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MangaWebApp.Models
{
    public class MangaService
    {
        HttpClient _client = new HttpClient();



        /**
      * Search for mangas
      */
        public IEnumerable<Manga> Search(SearchParams param)
        {
            string uri = "https://localhost:44380/api/mangas/search?PageIndex=" + param.PageIndex + "&nbResult=" + param.nbResult
                    + "&Title=" + param.Title + "&year=" + param.Year + "&price=" + param.Price + "&genre=" + param.Genre;
            var resp = _client.GetAsync(uri).Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Manga>>(resp.Content.ReadAsStringAsync().Result);
            return Enumerable.Empty<Manga>();
        }
        /**
         * Get all Mangas
         */
        public IEnumerable<Manga> Get()
        {
            var resp = _client.GetAsync("https://localhost:44380/api/mangas").Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Manga>>(resp.Content.ReadAsStringAsync().Result);
            return null;
        }

        /**
         * Get a specific Manga by ID
         */
        public Manga Get(int id)
        {
            var resp = _client.GetAsync("https://localhost:44380/api/mangas/" + id).Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Manga>(resp.Content.ReadAsStringAsync().Result);
            return null;
        }

        public IEnumerable<Comment> GetComments(int id)
        {
            var resp = _client.GetAsync("https://localhost:44380/api/mangas/" + id + "/comments").Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Comment>>(resp.Content.ReadAsStringAsync().Result);
            return null;
        }

        public IEnumerable<Rating> GetRatings(int id)
        {
            var resp = _client.GetAsync("https://localhost:44380/api/mangas/" + id + "/marks").Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Rating>>(resp.Content.ReadAsStringAsync().Result);
            return null;
        }

        public Manga CreateManga(Manga manga)
        {
            var uri = "https://localhost:44380/api/mangas";
            var content = JsonConvert.SerializeObject(manga);

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            var resp = _client.PostAsync(uri, httpContent).Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Manga>(resp.Content.ReadAsStringAsync().Result);
            return null;
        }

        public bool ModifyManga(Manga manga)
        {
            var uri = "https://localhost:44380/api/mangas/" + manga.Id;
            var content = JsonConvert.SerializeObject(manga);

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            var resp = _client.PutAsync(uri, httpContent).Result;

            if (resp.StatusCode == HttpStatusCode.NoContent)
                return true;
            return false;
        }

        public bool DeleteManga(int id)
        {
            var uri = "https://localhost:44380/api/mangas/" + id;

            var resp = _client.DeleteAsync(uri).Result;

            return JsonConvert.DeserializeObject<bool>(resp.Content.ReadAsStringAsync().Result);;
        }
    }
}
