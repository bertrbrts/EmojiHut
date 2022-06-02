using EmojiHut.Extensions;
using EmojiHut.Models;

namespace EmojiHut.ViewModels
{
    public class HomeViewModel : IHomeViewModel
    {
        public List<string?> Groups { get; set; }
        public List<Emoji> Emoji { get; set; }

        public HomeViewModel(IEmoji emoji)
        {
            Emoji = Task.Run(async () => await emoji.GetAllAsync()).Result;
            Groups = Emoji.Select(x => x.Group?.Split('-')[0]?.ToTitleCase()).Distinct().ToList();
        }
    }
}