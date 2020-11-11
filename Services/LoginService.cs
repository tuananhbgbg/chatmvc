
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;



namespace ChatMVC
{
    public class LoginService
    {
        private readonly IHttpClientFactory factory;
        private static string apiKey;

        public LoginService(IHttpClientFactory factory)
        {
            this.factory = factory;
        }

        public User Login(string login, string password)
        {
            var credentials = JsonConvert.SerializeObject(new { login, password });
            var requestContent = new StringContent(credentials, Encoding.Unicode, "application/json");
            var endpoint = "https://latest-chat.herokuapp.com/api/user/login";

            var response = factory.CreateClient().PostAsync(endpoint,requestContent).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var content = JsonConvert.DeserializeObject<User>(responseContent);
            apiKey = content.ApiKey;
            return content;
        }
        public string Logout()
        {
            var endpoint = "https://latest-chat.herokuapp.com/api/user/logout";
            var response = factory.CreateClient();
          
            response.DefaultRequestHeaders.Add("apikey", apiKey);
            var status = response.PostAsync(endpoint, null).Result;
            
            if (status.IsSuccessStatusCode)
            {
                apiKey = String.Empty;
                return apiKey;
            }
            return apiKey;
        }
        public User Register(string login,string password)
        {
            var credentials = JsonConvert.SerializeObject(new { login, password });
            var requestContent = new StringContent(credentials, Encoding.Unicode, "application/json");
            var endpoint = "https://latest-chat.herokuapp.com/api/user/register";

            var response = factory.CreateClient().PostAsync(endpoint, requestContent).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var content = JsonConvert.DeserializeObject<User>(responseContent);
                return content;
            }
            return new User();
        }
        public bool Update(string username, string avatarurl)
        {
            var updateInfo = JsonConvert.SerializeObject(new { username, avatarurl });
            var requestContent = new StringContent(updateInfo, Encoding.Unicode, "application/json");
            var endpoint = "https://latest-chat.herokuapp.com/api/user/update";
            requestContent.Headers.Add("apikey", apiKey);

            var response = factory.CreateClient().PostAsync(endpoint, requestContent).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }


    
    }
}
