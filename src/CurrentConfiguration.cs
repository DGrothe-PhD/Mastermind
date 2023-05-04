using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindVariante
{
    internal static class CurrentConfiguration
    {
        private static string languageCode = "en-GB";

        public static readonly List<string> Cultures = new()
        {
            "de-DE", "en-GB", "en-US"
        };


        public static void ApplyLanguage()
        {
            if(Cultures.Contains(languageCode))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(languageCode);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);
            }
        }

        public static void SetLanguage(string code)
        {
            if (Cultures.Contains(code))
            {
                languageCode = code;
            }
        }
    }

}
