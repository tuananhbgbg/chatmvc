
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChatMVC
{
    public class MessageService
    {
        private readonly HttpClient httpClient;
       

        public MessageService(IHttpClientFactory factory)
        {
            httpClient = factory.CreateClient();
            httpClient.BaseAddress = new Uri("https://latest-chat.herokuapp.com");
        }
        public void Register(string apikey)
        {
            if (String.IsNullOrEmpty(HeaderApiKey()))
            {
                httpClient.DefaultRequestHeaders.Add("apikey", apikey);
            }
        }
        public ChannelChat GetChannelAndMessages(int count)
        {
            string channelId = null;
            string channelSecret = null;
            var channelInfo = JsonConvert.SerializeObject(new { channelId, channelSecret, count });
            var requestContent = new StringContent(channelInfo, Encoding.Unicode, "application/json");
            var endpoint = "/api/channel/get-messages";

            var response = httpClient.PostAsync(endpoint, requestContent).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var channelChat =  JsonConvert.DeserializeObject<ChannelChat>(responseContent);
            channelChat.Messages = channelChat.Messages.OrderBy(m => m.Created).ToList();
            return channelChat;
        }
        public string HeaderApiKey()
        {
            return httpClient.DefaultRequestHeaders.FirstOrDefault().Value?.FirstOrDefault();
        }
        public void SendMsg(string content)
        {
            string channelId = null;
            string channelSecret = null;
            var channelInfo = JsonConvert.SerializeObject(new { channelId, channelSecret, content });
            var requestContent = new StringContent(channelInfo, Encoding.Unicode, "application/json");
            var endpoint = "/api/message";

            var response = httpClient.PostAsync(endpoint, requestContent).Result;
        }

    }
}
