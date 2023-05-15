using System.Globalization;

namespace MastermindVariante
{
    internal static class CurrentConfiguration
    {
        internal static Form1? Caller { get; set; }

        private static string languageCode = "en-GB";
        public static string Language { get => languageCode; }

        private static JsonWordLists? JsonWordLists;

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

            SetLanguage(Languages.FirstOrDefault(x => x.Value == ts?.Text)!.Key ?? "en-US");
            ApplyLanguage();
            SwitchWordListsToCurrentLanguage();
        }

        /// <summary>
        /// Look up current UI culture (system default) in list of available resources represented by `Cultures`.
        /// Use as Mastermind's initial UI language if corresponding resources file is present, otherwise English.
        /// </summary>
        public static void SetInitialLanguage()
        {
            string? lc = Cultures.FirstOrDefault(
                x => x.StartsWith(Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName)
            );

            if (String.IsNullOrEmpty(lc))
            {
                languageCode = "en-US";
            }
            else
            {
                languageCode = lc;
            }

            ApplyLanguage();
            SwitchWordListsToCurrentLanguage();
        }

        public static void ApplyLanguage()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(languageCode);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);
        }

        private static void SwitchWordListsToCurrentLanguage()
        {
            JsonWordLists = new JsonWordLists(languageCode);
            if (JsonWordLists != null)
            {
                Caller?.Evaluation?.SetWordLists(JsonWordLists?.CurrentWordLists);
            }
        }

        /// <summary>
        /// Set Mastermind's language property.
        /// </summary>
        /// <param name="code"></param>
        public static void SetLanguage(string code)
        {
            if (Cultures.Contains(code))
            {
                languageCode = code;
            }
        }
    }

}
