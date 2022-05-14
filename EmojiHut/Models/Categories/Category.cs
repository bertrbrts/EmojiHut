using Newtonsoft.Json;

namespace EmojiHut.Models
{
    public class Category : ModelBase, ICategory
    {
        /// <summary>
        /// Description of Category
        /// </summary>
        [JsonProperty("slug")]
        public string? Slug { get; set; }
        /// <summary>
        /// Sub Category
        /// </summary>
        [JsonProperty("subCategories")]
        public string[]? SubCategories { get; set; }
        [JsonIgnore]
        public override string PrimaryQueryString { get; set; } = $"categories?access_key={AccessKey}";
        [JsonIgnore]
        public override string FallbackDataPath { get; set; } = Configuration.GetValue<string>("DataPath:Categories");

        /// <summary>
        /// Returns all categories in dataset.
        /// </summary>
        /// <returns>List of Emoji</returns>
        public async Task<List<Category>> GetAllAsync()
        {
            List<Category> result;
            try
            {
                result = await GetAsync<Category>(PrimaryQueryString);
            }
            catch (JsonSerializationException)
            {
                result = await GetFallbackDataAsync();
            }

            return result;
        }
        /// <summary>
        /// Returns all categories from fallback dataset.
        /// </summary>
        /// <returns></returns>
        private async Task<List<Category>> GetFallbackDataAsync() =>
            await GetFallbackDataAsync<Category>(FallbackDataPath);
    }
}