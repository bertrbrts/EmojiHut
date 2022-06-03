using EmojiHut.Models;
using System.Collections.Generic;

namespace EmojiHut.ViewModels
{
    public interface IHomeViewModel
    {
        List<string> Groups { get; set; }
        List<Emoji> Emoji { get; set; }
    }
}