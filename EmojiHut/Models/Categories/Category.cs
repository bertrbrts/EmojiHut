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
        /// <summary>
        /// Returns all categories in dataset.
        /// </summary>
        /// <returns>List of Emoji</returns>
        public async Task<List<Category>> GetAllAsync() =>
            await GetAsync<Category>($"categories?access_key={AccessKey}");
        /// <summary>
        /// Returns all categories from fallback dataset.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Category>> GetFallbackDataAsync() =>
            await GetFallbackDataAsync<Category>(Configuration.GetValue<string>("DataPath:Categories"));
    }
}