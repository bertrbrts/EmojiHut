using EmojiHut.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmojiHut.Models
{
    public class Emoji : ModelBase, IEmoji
    {
        private string _slug;
        /// <summary>
        /// Description of Emoji
        /// </summary>
        [JsonProperty("slug")]
        public string Slug
        {
            get
            {
                if (_slug != null && _slug.Contains(':'))
                    _slug = _slug.Split(':')[0];

                return _slug?.ToTitleCase() ?? string.Empty;
            }
            set => _slug = value;
        }

        /// <summary>
        /// Unicode Character
        /// </summary>
        [JsonProperty("character")]
        public string Character { get; set; }

        private string _unicodeName;
        /// <summary>
        /// Unicode Name
        /// </summary>
        [JsonProperty("unicodeName")]
        public string UnicodeName
        {
            get => _unicodeName?.ToTitleCase() ?? string.Empty;
            set => _unicodeName = value;
        }
        /// <summary>
        /// Code Point
        /// </summary>
        [JsonProperty("codePoint")]
        public string CodePoint { get; set; }
        /// <summary>
        /// Group
        /// </summary>
        [JsonProperty("group")]
        public string Group { get; set; }
        /// <summary>
        /// Sub Group
        /// </summary>
        [JsonProperty("subGroup")]
        public string SubGroup { get; set; }

        [JsonIgnore]
        public override string PrimaryQueryString { get; set; } = $"emojis?access_key={AccessKey}";
        [JsonIgnore]
        public override string FallbackDataPath { get; set; } = "~/Emoji/Emoji.json";

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
            await GetFallbackDataAsync<Emoji>(FallbackDataPath);
    }
}