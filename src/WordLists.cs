//using Newtonsoft.Json;
using Lang;
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
        //this gonna be jsonified.
        public string[]? WordsWith3 { get; set; }
        //    =
        //{
        //    "SEN", "YEN", "GEL", "ULF", "RAT", "TOP", "POL", "DUR", "GER", "KUH", "EIN", "SOG",
        //    "FIT", "FEE", "ZOO", "OPA", "TEE", "PRO", "OHR", "KUR", "UHR", "EIS", "ELF", "ZAR",
        //    "PER", "SIE", "ALF", "GAS", "WEM", "BUB", "RAP", "WEH", "WOK", "GAR", "LUV", "LAU",
        //    "NUN", "PIN", "GIN", "NIE", "JET", "REN", "VOR", "LEE", "VON", "RAR", "MIR", "ROM",
        //    "HUT", "TAL", "NEO", "OLM", "HOF", "BEN", "ZUG", "MIT", "DEN", "RUM", "RAD", "ULM",
        //    "FAN", "SAM", "ORT", "ROH", "ZEN", "AMT", "OMA", "ICH", "WIE", "INN", "ZEH", "RUF",
        //    "MUT", "NUR", "SEE", "TOM", "POP", "LAB", "CER", "MAX", "UTA", "UND", "BUS", "AHN",
        //    "UNS", "WAL", "GNU", "WIR", "BOR", "REX", "LAR", "JOD", "WAS", "IHR", "HIT", "HUF",
        //    "QUA", "HER", "TOR", "ROT", "MAL", "ROY", "DAS", "MAI", "URI", "URD", "WER", "DOM",
        //    "OFF", "AUF", "MET", "PUR", "BAR", "DAR", "OHM", "NAH", "AST", "WEG", "KLO", "HIN",
        //    "EHE", "HAI", "GUT"
        //};

        public string[]? WordsWith4 { get; set; }
        //    =
        //{
        //    "FLUG", "MULL", "WELS", "BAUM", "HUND", "YETI", "WAHL", "SALZ", "EINS", "FELS",
        //    "KINO", "EIBE", "HEIM", "EULE", "ZIMT", "CLAN", "QUER", "BILD", "OTTO", "BALL",
        //    "BAHN", "JEDE", "CLIP", "MOPS", "OSLO", "JUDE", "DANK", "EGGE", "CHEF", "NAIV",
        //    "KALT", "LIEB", "MEMO", "VASE", "ZEHN", "PUCK", "LOSE", "WITZ", "JULI", "LAUF",
        //    "SENF", "FARN", "HIRN", "AUTO", "STEG", "SEKT", "MAUS", "VIER", "PRAG", "GURT",
        //    "EFEU", "HANF", "BRAV", "FILM", "DORF", "BEIN", "WILD", "LEIM", "NORD", "EGEL",
        //    "SNOB", "IGEL", "OHNE", "STAU", "LAMM", "ABER", "LUMP", "KITT", "FUSS", "HOLD",
        //    "TUER", "SAND", "IRRE", "ZART", "NERD", "DREI", "TIEF", "KURZ", "WALD", "PIEP",
        //    "FORT", "WEIN", "BIER", "MAIL", "FROH", "YOGA", "LANG", "FUNK", "URDU", "ZWEI",
        //    "GROG", "MOLL", "OBST", "DEIN", "ANIS", "KAUM", "IMME", "WARM", "MEHL", "AMEN",
        //    "NASS", "MATT", "TORF", "EIER", "SACK", "KIND", "WATT", "RUHR", "REIS", "SIEB",
        //    "WIEN", "GRAZ", "ZAEH", "DUFT", "AULA", "FUER", "BALZ", "WALZ", "RUHE", "RUTH"
        //};

        public string[]? WordsWith5 { get; set; }
        //    =
        //{
        //    "KEGEL", "SANFT", "SEHNE", "FELGE", "LIEBE", "METER", "ABEND", "MASSE", "OCKER",
        //    "PISTE", "ERPEL", "NORNE", "SAUNA", "JACKE", "KETTE", "KISTE", "DEINE", "DAVOS",
        //    "CELLO", "DIESE", "MAERZ", "KELCH", "LAUNE", "QUARK", "ZANGE", "USCHI", "PASTA",
        //    "APRIL", "JESUS", "STUTE", "BIBER", "POKAL", "KARTE", "FLIRT", "ESSEN", "HEISS",
        //    "TANGO", "LASSO", "MEILE", "IMMER", "SAMBA", "LAUBE", "STAUB", "HONIG", "SCHAL",
        //    "SPEZI", "FARAD", "BULLE", "KNAPP", "QUELL", "PIZZA", "EMSIG", "HAFER", "MIETE",
        //    "CHLOR", "DAUNE", "NIZZA", "LEISE", "EIDAM", "PUMPE", "PFERD", "KREIS", "BIRNE",
        //    "ORDER", "BIRGT", "DOLDE", "GURKE", "EINST", "MATTE", "NACHT", "KAESE", "PEDAL",
        //    "JENER", "AUGEN", "DACHS", "ERIKA", "WALZE", "NOPPE", "PHLOX", "EIFER", "CHILI",
        //    "STEIN", "INNIG", "AMBRA", "FARSI", "FIRST", "GUAVE", "CIRCA", "KANTE", "GRUEN",
        //    "MAKEL", "DURST", "DECKE", "KATER", "BIENE", "BINGO", "MITTE", "KEKSE", "GENIE"
        //};
    }
}