using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MangaWebApp.Models
{
    public class CommentService
    {
        HttpClient _client = new HttpClient();

        /**
         * Get a specific Comment by ID
         */
        public Comment Get(int id)
        {
            var resp = _client.GetAsync("https://localhost:44380/api/comments/" + id).Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Comment>(resp.Content.ReadAsStringAsync().Result);
            return null;
        }

        public Comment CreateComment(Comment comment)
        {
            var uri = "https://localhost:44380/api/comments";
            var content = JsonConvert.SerializeObject(comment);

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            var resp = _client.PostAsync(uri, httpContent).Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Comment>(resp.Content.ReadAsStringAsync().Result);
            return null;
        }
    }
}
