using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WikiSurfModel.Utils
{
    public static class HtmlUtils
    {
        public static string UrlEncode(string word)
        {
            return WebUtility.UrlEncode(word);
        }

        public static string UrlDecode(string word)
        {
            return WebUtility.UrlDecode(word);
        }

        public static string HtmlEncode(string word)
        {
            return WebUtility.HtmlEncode(word);
        }

        public static string HtmlDecode(string word)
        {
            return WebUtility.HtmlDecode(word);
        }
    }
}
