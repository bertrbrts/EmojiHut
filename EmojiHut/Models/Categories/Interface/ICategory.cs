
namespace EmojiHut.Models
{
    public interface ICategory
    {
        string? Slug { get; set; }
        string[]? SubCategories { get; set; }

        Task<List<Category>> GetAllAsync();
        Task<List<Category>> GetFallbackDataAsync();
    }
}