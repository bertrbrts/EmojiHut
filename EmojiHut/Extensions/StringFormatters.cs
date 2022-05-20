using System.Globalization;

namespace EmojiHut.Extensions
{
    public static class StringFormatters
    {
        public static string ToTitleCase(this string s)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
        }
    }
}