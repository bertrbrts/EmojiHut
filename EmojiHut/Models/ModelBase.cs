using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace EmojiHut.Models
{
    public abstract class ModelBase : IModel
    {
        private readonly string _baseURL = Configuration.GetValue<string>("ApiSettings:BaseURL");

        protected readonly static IConfiguration Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        protected readonly static string AccessKey = Configuration.GetValue<string>("ApiSettings:AccessKey");
        protected readonly static string FallbackBasePath = Configuration.GetValue<string>("DataPath:BasePath");

        public abstract string PrimaryQueryString { get; set; }
        public abstract string FallbackDataPath { get; set; }

        public async Task<List<T>> GetAsync<T>(string query) 
        {
            using HttpClient? client = new() { BaseAddress = new Uri(_baseURL) };
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

        public async Task<List<T>> GetFallbackDataAsync<T>(string dataPath)
        {
            using var reader = new StreamReader($"{FallbackBasePath}{dataPath}");
            string json = await reader.ReadToEndAsync();
            return Deserialize<T>(json);
        }

        private static List<T> Deserialize<T>(string json) 
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