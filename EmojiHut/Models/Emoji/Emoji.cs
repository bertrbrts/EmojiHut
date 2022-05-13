using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace EmojiHut.Models
{
    public class Emoji : IEmoji
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("slug")]
        public string? Slug { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("character")]
        public string? Character { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("unicodeName")]
        public string? UnicodeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("codePoint")]
        public string? CodePoint { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("group")]
        public string? Group { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("subGroup")]
        public string? SubGroup { get; set; }

        private readonly IConfiguration _configuration;

        private readonly string baseURL;
        private readonly string accessKey;

        public Emoji()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            baseURL = _configuration.GetValue<string>("ApiSettings:BaseURL");
            accessKey = _configuration.GetValue<string>("ApiSettings:AccessKey");
        }

        public async Task<List<Emoji>> GetAllAsync() =>
            await GetAsync($"emojis?access_key={accessKey}");

        private async Task<List<Emoji>> GetAsync(string query)
        {
            using HttpClient? client = new() { BaseAddress = new Uri(baseURL) };
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using HttpResponseMessage res = await client.GetAsync(query);

            if (res.IsSuccessStatusCode)
            {
                string response = await res.Content.ReadAsStringAsync();
                return Deserialize(response);
            }

            return new List<Emoji>();
        }

        public async Task<List<Emoji>> GetFallbackDataAsync()
        {
            string basePath = _configuration.GetValue<string>("DataPath:BasePath");
            string emojiPath = _configuration.GetValue<string>("DataPath:Emojis");

            using var reader = new StreamReader($"{basePath}{emojiPath}");
            string json = await reader.ReadToEndAsync();
            return Deserialize(json);
        }

        private static List<Emoji> Deserialize(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Emoji>>(json) ?? new List<Emoji>();
            }
            catch (JsonSerializationException e)
            {
                throw new JsonSerializationException($"JsonSerializationExcpetion: {e.Message}\n" +
                                                     $"Response: {json}");
            }
        }
    }
}