using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmojiHut.Models
{
    public interface IEmoji
    {
        string Character { get; set; }
        string CodePoint { get; set; }
        string Group { get; set; }
        string Slug { get; set; }
        string SubGroup { get; set; }
        string UnicodeName { get; set; }

        Task<List<Emoji>> GetAllAsync();
    }
}