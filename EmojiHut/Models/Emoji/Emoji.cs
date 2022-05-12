using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace EmojiHut.Models
{
    public class Emoji
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

        private readonly string baseURL = "https://emoji-api.com/";
        private readonly string accessKey = "08d9158028fcdddd8927c3e895ad5e84d6f4c9c0";

        // https://emoji-api.com/emojis?access_key=08d9158028fcdddd8927c3e895ad5e84d6f4c9c0
        public async Task<List<Emoji>> GetAllAsync() => 
            await Get($"emojis?access_key={accessKey}");

        public async Task<List<Emoji>> GetSingleAsync() =>
            await Get($"emojis/grinning-squinting-face?access_key={accessKey}");

        private async Task<List<Emoji>> Get(string query)
        {
            List<Emoji>? result = new();

            using (HttpClient? client = new())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage res = await client.GetAsync(query);

                if (res.IsSuccessStatusCode)
                {
                    string response = await res.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<Emoji>>(response);
                }
            }

            return result ?? new List<Emoji>();
        }       
    }
}