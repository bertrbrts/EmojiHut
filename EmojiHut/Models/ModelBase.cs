using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace EmojiHut.Models
{
    public abstract class ModelBase
    {
        protected static IConfiguration Configuration = 
            new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        protected string BaseURL = Configuration.GetValue<string>("ApiSettings:BaseURL");
        protected string AccessKey = Configuration.GetValue<string>("ApiSettings:AccessKey");
        protected string FallbackBasePath = Configuration.GetValue<string>("DataPath:BasePath");

        protected virtual async Task<List<T>> GetAsync<T>(string query) where T : class
        {
            using HttpClient? client = new() { BaseAddress = new Uri(BaseURL) };
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using HttpResponseMessage res = await client.GetAsync(query);

            if (res.IsSuccessStatusCode)
            {
                string response = await res.Content.ReadAsStringAsync();
                return Deserialize<T>(response);
            }

            return new List<T>();
        }

        protected virtual async Task<List<T>> GetFallbackDataAsync<T>(string dataPath) where T : class
        {
            using var reader = new StreamReader($"{FallbackBasePath}{dataPath}");
            string json = await reader.ReadToEndAsync();
            return Deserialize<T>(json);
        }

        private static List<T> Deserialize<T>(string json) where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
            }
            catch (JsonSerializationException e)
            {
                throw new JsonSerializationException($"JsonSerializationExcpetion: {e.Message}\n" +
                                                     $"Response: {json}");
            }
        }
    }
}