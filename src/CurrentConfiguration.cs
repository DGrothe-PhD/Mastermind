using System.Globalization;

namespace MastermindVariante
{
    internal static class CurrentConfiguration
    {
        private static string languageCode = "en-GB";

        public static readonly List<string> Cultures = new()
        {
            "de-DE", 
            "en-GB", "en-US"
        };

        public static void SetInitialLanguage()
        {
            // Tries to retrieve current UI culture (system default) in list of available resources represented by `Cultures`.
            // If a system's language is not available by this application, English is used.
            string? lc = Cultures.FirstOrDefault(
                x => x.StartsWith(Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName)
            );

            if(String.IsNullOrEmpty( lc ) )
            {
                languageCode = "en-US";
            }
            else
            {
                languageCode = lc;
            }

            ApplyLanguage();
        }

        public static void ApplyLanguage()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(languageCode);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);
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
