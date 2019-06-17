using BrainPowerApp.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BrainPowerApp.ToolBox
{
    class ApiClient
    {

        public async Task<string> GetRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            WebResponse response = await request.GetResponseAsync();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public async Task<string> PostRequest(string url, Player player)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            var content = new StringContent(JsonConvert.SerializeObject(player), Encoding.UTF8, "application/json");
            var result = await client.PostAsync(url, content);
            string resultContent = await result.Content.ReadAsStringAsync();
            return resultContent;
        }
    }
}
