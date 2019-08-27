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
    public class UserService
    {
        HttpClient _client = new HttpClient();

        /**
         * Get a specific User by ID
         */
        public User Get(int id)
        {
            var resp = _client.GetAsync("https://localhost:44380/api/users/" + id).Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<User>(resp.Content.ReadAsStringAsync().Result);
            return null;
        }

        public User GetByEmail(string email)
        {
            var uri = "https://localhost:44380/api/users/getbyemail?email=" + email;

            HttpContent httpContent = new StringContent(String.Empty);

            var resp = _client.PostAsync(uri, httpContent).Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<User>(resp.Content.ReadAsStringAsync().Result);
            return null;
        }

        public User GetByUsernameAndPassword(string name, string password)
        {
            //System.Diagnostics.Debug.WriteLine();
            var uri = "https://localhost:44380/api/users/connect?name=" + name + "&password=" + password;

            HttpContent httpContent = new StringContent(String.Empty);

            var resp = _client.PostAsync(uri, httpContent).Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<User>(resp.Content.ReadAsStringAsync().Result);
            return null;
        }

        public User CreateUser(string name, string email, string password)
        {
            var uri = "https://localhost:44380/api/users";
            var content = JsonConvert.SerializeObject(new User() { Name=name, Email=email, Password=password });

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            var resp = _client.PostAsync(uri, httpContent).Result;

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<User>(resp.Content.ReadAsStringAsync().Result);
            return null;
        }

        public bool ModifyUser(User user)
        {
            var uri = "https://localhost:44380/api/users/" + user.Id;
            var content = JsonConvert.SerializeObject(user);

            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            var resp = _client.PutAsync(uri, httpContent).Result;

            if (resp.StatusCode == HttpStatusCode.NoContent)
                return true;
            return false;
        }
    }
}
