using Newtonsoft.Json;

namespace EmojiHut.Models
{
    public class Emoji : ModelBase, IEmoji
    {
        /// <summary>
        /// Description of Emoji
        /// </summary>
        [JsonProperty("slug")]
        public string? Slug { get; set; }
        /// <summary>
        /// Unicode Character
        /// </summary>
        [JsonProperty("character")]
        public string? Character { get; set; }
        /// <summary>
        /// Unicode Name
        /// </summary>
        [JsonProperty("unicodeName")]
        public string? UnicodeName { get; set; }
        /// <summary>
        /// Code Point
        /// </summary>
        [JsonProperty("codePoint")]
        public string? CodePoint { get; set; }
        /// <summary>
        /// Group
        /// </summary>
        [JsonProperty("group")]
        public string? Group { get; set; }
        /// <summary>
        /// Sub Group
        /// </summary>
        [JsonProperty("subGroup")]
        public string? SubGroup { get; set; }

        [JsonIgnore]
        public override string PrimaryQueryString { get; set; } = $"emojis?access_key={AccessKey}";
        [JsonIgnore]
        public override string FallbackDataPath { get; set; } = Configuration.GetValue<string>("DataPath:Emojis");

        /// <summary>
        /// Returns all emojis in dataset.
        /// </summary>
        /// <returns>List of Emoji</returns>
        public async Task<List<Emoji>> GetAllAsync()
        {
            List<Emoji> result;
            try
            {
                result = await GetAsync<Emoji>(PrimaryQueryString);
            }
            catch (JsonSerializationException)
            {
                result = await GetFallbackDataAsync();
            }

            return result;
        }

        /// <summary>
        /// Returns all emojis from fallback dataset.
        /// </summary>
        /// <returns></returns>
        private async Task<List<Emoji>> GetFallbackDataAsync() =>
            await GetFallbackDataAsync<Emoji>(Configuration.GetValue<string>("DataPath:Emojis"));
    }
}