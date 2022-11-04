using System.Text;

namespace UrlShortener.Client.Services
{
    public static class UrlShortenerService
    {
        private static readonly string _alphabet = "23456789bcdfghjkmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ-_";
        private static readonly int _base = _alphabet.Length;

        public static string Encode(string url)
        {
            int num = GetHash(url);
            var sb = new StringBuilder();
            while (num > 0)
            {
                sb.Insert(0, _alphabet.ElementAt(num % _base));
                num = num / _base;
            }

            string shortUrl = $"https://shrtnr.io/{sb}";
            return shortUrl;
        }

        private static int GetHash(string str)
        {
            var num = 0;
            for (var i = 0; i < str.Length; i++)
            {
                num = num * _base + _alphabet.IndexOf(str.ElementAt(i));
            }
            return Math.Abs(num);
        }
    }
}
