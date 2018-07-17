using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace SerkoTestAPI.Models
{
    public class JsonResponseBuilder
    {
        public HttpResponseMessage BuildResponse(HttpResponseMessage response, object data)
        {
            var dataAsJson = JsonConvert.SerializeObject(data,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                });

            response.Content = new StringContent(dataAsJson, Encoding.UTF8, "application/json");
            return response;
        }
    }
}