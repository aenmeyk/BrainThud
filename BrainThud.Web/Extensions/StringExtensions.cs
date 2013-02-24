using System.Text.RegularExpressions;

namespace BrainThud.Web.Extensions
{
    public static class StringExtensions
    {
        public static string GenerateSlug(this string phrase, int length = 1024)
        {
            if(string.IsNullOrEmpty(phrase)) return phrase;

            var str = phrase.RemoveAccent().ToLower();

            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");

            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();

            // cut and trim 
            str = str.Substring(0, str.Length <= length ? str.Length : length).Trim();
            
            // hyphens  
            str = Regex.Replace(str, @"\s", "-");  

            return str;
        }

        public static string RemoveAccent(this string txt)
        {
            var bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        } 
    }
}