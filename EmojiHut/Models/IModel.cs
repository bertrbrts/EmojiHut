namespace EmojiHut.Models
{
    public interface IModel
    {
        Task<List<T>> GetAsync<T>(string query);
        Task<List<T>> GetFallbackDataAsync<T>(string dataPath);
    }
}
