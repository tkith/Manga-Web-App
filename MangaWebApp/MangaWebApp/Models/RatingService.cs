using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MangaWebApp.Models
{
    public class RatingService
    {
        HttpClient _client = new HttpClient();

        /**
         * Get a specific Rating by ID
         */
        public Rating Get(int id)
        {
            var resp = _client.GetAsync("https://localhost:44380/api/marks/" + id).Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Rating>(resp.Content.ReadAsStringAsync().Result);
            return null;
        }

        public Rating GetRatingByUserIdAndMangaId(int userId, int mangaId)
        {
            var resp = _client.GetAsync("https://localhost:44380/api/marks/userIdMangaId?userId=" + userId + "&mangaId=" + mangaId).Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Rating>(resp.Content.ReadAsStringAsync().Result);
            return null;
        }

        public Rating CreateRating(Rating rating)
        {
            var uri = "https://localhost:44380/api/marks";
            var content = JsonConvert.SerializeObject(rating);

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            var resp = _client.PostAsync(uri, httpContent).Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Rating>(resp.Content.ReadAsStringAsync().Result);
            return null;
        }
    }
}
