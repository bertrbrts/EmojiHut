using EmojiHut.Models;

namespace EmojiHut.ViewModels
{
    public class HomeViewModel : IHomeViewModel
    {
        public List<Category> Category { get; set; }
        public List<Emoji> Emoji { get; set; }

        public HomeViewModel(ICategory category, IEmoji emoji)
        {
            Emoji = Task.Run(async () => await emoji.GetAllAsync()).Result;
            Category = Task.Run(async () => await category.GetAllAsync()).Result;
        }
    }
}