using System.Globalization;

namespace MastermindVariante
{
    internal static class CurrentConfiguration
    {
        private static string languageCode = "en-GB";
        public static string Language { get => languageCode; }

        public static List<string> Cultures => Languages.Keys.ToList();

        public static readonly Dictionary<string, string> Languages = new()
        {
            {"de-DE", "Deutsch" }, {"fr-FR", "Français" }, {"en-GB", "English" }, {"en-US", "Default" }
        };

        /// <summary>
        /// Returns an array of controls (e. g. radio buttons), one for each language implemented.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>T[]</returns>
        public static T[] GetControls<T>() where T : Control, new()
        {
            T[] ts = new T[Languages.Count];
            for (int i = 0; i < Languages.Count; i++)
            {
                ts[i] = new T()
                {
                    Text = Languages[Cultures[i]],
                    Name = "SwitchLanguage" + typeof(T).Name + "0" + i,
                };

                ts[i].Click += LanguageSwitch_Click;
            }
            return ts;
        }

        /// <summary>
        /// Returns an array of ToolStripItems (e. g. ContextMenuStrip sub-items), one for each language implemented.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>T[]</returns>
        public static T[] GetToolStrips<T>() where T : ToolStripItem, new()
        {
            T[] ts = new T[Languages.Count];
            for (int i = 0; i < Languages.Count; i++)
            {
                ts[i] = new T()
                {
                    Text = Languages[Cultures[i]],
                    Name = "SwitchLanguage" + typeof(T).Name + "0" + i,
                };

                ts[i].Click += LanguageSwitch_Click;
            }
            return ts;
        }

        internal static void LanguageSwitch_Click(object? sender, EventArgs e)
        {
            ToolStripMenuItem? ts = sender as ToolStripMenuItem;

            SetLanguage(Languages.FirstOrDefault(x=> x.Value == ts?.Text)!.Key ?? "en-US");
            ApplyLanguage();
        }

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
