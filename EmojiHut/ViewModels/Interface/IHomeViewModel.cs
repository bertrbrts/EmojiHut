using EmojiHut.Models;

namespace EmojiHut.ViewModels
{
    public interface IHomeViewModel
    {
        public List<string?> Groups { get; set; }
        //List<Category> Category { get; set; }
        List<Emoji> Emoji { get; set; }
    }
}