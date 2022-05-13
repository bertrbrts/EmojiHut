using EmojiHut.Models;

namespace EmojiHut.ViewModels
{
    public class HomeViewModel
    {
        private ICategory _category;
        private IEmoji _emoji;
        
        public HomeViewModel(ICategory category, IEmoji emoji)
        {
            _category = category;
            _emoji = emoji;
        }
    }
}
