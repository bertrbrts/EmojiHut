using EmojiHut.Models;

namespace EmojiHut.ViewModels
{
    public interface IHomeViewModel
    {
        List<Category> Category { get; set; }
        List<Emoji> Emoji { get; set; }
    }
}