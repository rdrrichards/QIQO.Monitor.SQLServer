using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QIQO.Monitor.Service
{
    public interface IHttpClientHelper
    {
        Task<T> Get<T>(string url);
        Task<string> Post(string url, object instance);
        Task<string> Post(string url, HttpContent content);
    }
    public class HttpClientHelper : IHttpClientHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string clientName = "QIQOMonitor";

        public HttpClientHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        /// <summary>
        /// GETs from the specified URL and deserializes the JSON response as an instance of the specified type.
        /// </summary>
        public async Task<T> Get<T>(string url)
        {
            var client = _httpClientFactory.CreateClient(clientName);
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var contentAsString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(contentAsString);
        }

        /// <summary>
        /// Serializes an object as JSON and POSTs the result to the specified URL.
        /// </summary>
        public Task<string> Post(string url, object instance)
        {
            var data = JsonConvert.SerializeObject(instance, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            return Post(url, content);
        }

        /// <summary>
        /// POSTs the specified content to the specified URL and returns the location header from the response,
        /// which is the URL of the newly created resource.
        /// </summary>
        public async Task<string> Post(string url, HttpContent content)
        {
            var client = _httpClientFactory.CreateClient(clientName);
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            return response.StatusCode.ToString();
        }
    }
}
