using System.Text.Json;

namespace MastermindVariante
{
    public class JsonWordLists
    {
        public string? JsonFile { get; private set; }
        public WordLists? CurrentWordLists { get; set; }
        public JsonWordLists(string languageCode)
        {
            CurrentWordLists = new WordLists();
            SetWordList(languageCode);
        }

        /// <summary>
        /// Set the word lists according to the language code.
        /// </summary>
        /// <param name="languageCode"></param>
        public void SetWordList(string languageCode)
        {
            try
            {
                JsonFile = "locales/WordLists_" + languageCode[0..2] + ".json";
                string jsonString = File.ReadAllText(JsonFile);
                
                //It has to be dynamic, otherwise it's not overwriting the field content herein.
                dynamic wl = JsonSerializer.Deserialize<WordLists>(jsonString)!;
                this.CurrentWordLists = wl;
            }
            catch(FileNotFoundException)
            {
                MessageBox.Show($"File {JsonFile} is missing.", "Json Failure", MessageBoxButtons.OKCancel);
            }
            catch(JsonException jex)
            {
                MessageBox.Show($"File {JsonFile} is not properly formatted as a json.\r\n"
                    +$"{jex.Message}","Json Failure", MessageBoxButtons.OKCancel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }

    public class WordLists
    {
        public string[]? WordsWith3 { get; set; }
        public string[]? WordsWith4 { get; set; }
        public string[]? WordsWith5 { get; set; }
    }
}