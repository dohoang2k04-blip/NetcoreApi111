using System.Text.Json;
 
namespace csmvc.Services
{
    public class ExternalService
    {
        private readonly HttpClient _httpClient;
 
        public ExternalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Dictionary<string, JsonElement> sendGetRequest(string apiUrl)
        {
            // 1. Make HTTP request (blocking)
            var response = _httpClient.GetAsync(apiUrl).Result;
            response.EnsureSuccessStatusCode();
            // 2. Read response body (blocking)
            var json = response.Content.ReadAsStringAsync().Result;
            // 3. Deserialize JSON to dictionary
            var root = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);
            return root!;
        }
        public JsonElement sendGetRequestElement(string url)
        {
            using HttpClient client = new HttpClient();
            string json = client.GetStringAsync(url).Result;

            JsonDocument doc = JsonDocument.Parse(json);
            return doc.RootElement.Clone();
        }

    }
}