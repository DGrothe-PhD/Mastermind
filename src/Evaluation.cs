using Lang;

namespace MastermindVariante
{
    public struct Points : IComparable<Points>
    {
        public short blackpins;
        public short whitepins;

        public Points(short blacks, short whites)
        {
            this.blackpins = blacks;
            this.whitepins = whites;
        }

        public int CompareTo(Points other)
        {
            if (this.blackpins < other.blackpins) return -1;
            if (this.blackpins == other.blackpins)
                return this.whitepins.CompareTo(other.whitepins);
            else return 1;
        }
    }
    public class Evaluation
    {
        private string solution = "";
        private short wordLength;
        private readonly Form1 caller;

        public Evaluation(Form1 caller)
        {
            this.caller = caller;
            wordLength = caller.WordLength;
        }

        public void SetSolution()
        {
            wordLength = caller.WordLength;
            solution = wordLength switch
            {
                3 => WordsWith3.Pick(),
                5 => WordsWith5.Pick(),
                _ => WordsWith4.Pick(),
            };
        }

        public static readonly string[] WordsWith3 =
        {
            "SEN", "YEN", "GEL", "ULF", "RAT", "TOP", "POL", "DUR", "GER", "KUH", "EIN", "SOG",
            "FIT", "FEE", "ZOO", "OPA", "TEE", "PRO", "OHR", "KUR", "UHR", "EIS", "ELF", "ZAR",
            "PER", "SIE", "ALF", "GAS", "WEM", "BUB", "RAP", "WEH", "WOK", "GAR", "LUV", "LAU",
            "NUN", "PIN", "GIN", "NIE", "JET", "REN", "VOR", "LEE", "VON", "RAR", "MIR", "ROM",
            "HUT", "TAL", "NEO", "OLM", "HOF", "BEN", "ZUG", "MIT", "DEN", "RUM", "RAD", "ULM",
            "FAN", "SAM", "ORT", "ROH", "ZEN", "AMT", "OMA", "ICH", "WIE", "INN", "ZEH", "RUF",
            "MUT", "NUR", "SEE", "TOM", "POP", "LAB", "CER", "MAX", "UTA", "UND", "BUS", "AHN",
            "UNS", "WAL", "GNU", "WIR", "BOR", "REX", "LAR", "JOD", "WAS", "IHR", "HIT", "HUF",
            "QUA", "HER", "TOR", "ROT", "MAL", "ROY", "DAS", "MAI", "URI", "URD", "WER", "DOM",
            "OFF", "AUF", "MET", "PUR", "BAR", "DAR", "OHM", "NAH", "AST", "WEG", "KLO", "HIN",
            "EHE", "HAI", "GUT"
        };

        public static readonly string[] WordsWith4 =
        {
            "FLUG", "MULL", "WELS", "BAUM", "HUND", "YETI", "WAHL", "SALZ", "EINS", "FELS",
            "KINO", "EIBE", "HEIM", "EULE", "ZIMT", "CLAN", "QUER", "BILD", "OTTO", "BALL",
            "BAHN", "JEDE", "CLIP", "MOPS", "OSLO", "JUDE", "DANK", "EGGE", "CHEF", "NAIV",
            "KALT", "LIEB", "MEMO", "VASE", "ZEHN", "PUCK", "LOSE", "WITZ", "JULI", "LAUF",
            "SENF", "FARN", "HIRN", "AUTO", "STEG", "SEKT", "MAUS", "VIER", "PRAG", "GURT",
            "EFEU", "HANF", "BRAV", "FILM", "DORF", "BEIN", "WILD", "LEIM", "NORD", "EGEL",
            "SNOB", "IGEL", "OHNE", "STAU", "LAMM", "ABER", "LUMP", "KITT", "FUSS", "HOLD",
            "TUER", "SAND", "IRRE", "ZART", "NERD", "DREI", "TIEF", "KURZ", "WALD", "PIEP",
            "FORT", "WEIN", "BIER", "MAIL", "FROH", "YOGA", "LANG", "FUNK", "URDU", "ZWEI",
            "GROG", "MOLL", "OBST", "DEIN", "ANIS", "KAUM", "IMME", "WARM", "MEHL", "AMEN",
            "NASS", "MATT", "TORF", "EIER", "SACK", "KIND", "WATT", "RUHR", "REIS", "SIEB",
            "WIEN", "GRAZ", "ZAEH", "DUFT", "AULA", "FUER", "BALZ", "WALZ", "RUHE", "RUTH"
        };

        public static readonly string[] WordsWith5 =
        {
            "KEGEL", "SANFT", "SEHNE", "FELGE", "LIEBE", "METER", "ABEND", "MASSE", "OCKER",
            "PISTE", "ERPEL", "NORNE", "SAUNA", "JACKE", "KETTE", "KISTE", "DEINE", "DAVOS",
            "CELLO", "DIESE", "MAERZ", "KELCH", "LAUNE", "QUARK", "ZANGE", "USCHI", "PASTA",
            "APRIL", "JESUS", "STUTE", "BIBER", "POKAL", "KARTE", "FLIRT", "ESSEN", "HEISS",
            "TANGO", "LASSO", "MEILE", "IMMER", "SAMBA", "LAUBE", "STAUB", "HONIG", "SCHAL",
            "SPEZI", "FARAD", "BULLE", "KNAPP", "QUELL", "PIZZA", "EMSIG", "HAFER", "MIETE",
            "CHLOR", "DAUNE", "NIZZA", "LEISE", "EIDAM", "PUMPE", "PFERD", "KREIS", "BIRNE",
            "ORDER", "BIRGT", "DOLDE", "GURKE", "EINST", "MATTE", "NACHT", "KAESE", "PEDAL",
            "JENER", "AUGEN", "DACHS", "ERIKA", "WALZE", "NOPPE", "PHLOX", "EIFER", "CHILI",
            "STEIN", "INNIG", "AMBRA", "FARSI", "FIRST", "GUAVE", "CIRCA", "KANTE", "GRUEN",
            "MAKEL", "DURST", "DECKE", "KATER", "BIENE", "BINGO", "MITTE", "KEKSE", "GENIE"
        };

        public Points WordPoints(string guessedWord)
        {
            short blackPins = 0;
            short whitePins = 0;
            char[] solutionCopy = solution.ToCharArray();
            char[] guessed = guessedWord.ToCharArray();

            for (short i = 0; i < guessed.Length; i++)
            {
                if (guessedWord[i] == solutionCopy[i])
                {
                    blackPins++;
                    solutionCopy[i] = '_';
                    guessed[i] = '_';
                }
            }
            for (short i = 0; i < guessed.Length; i++)
            {
                if (guessed[i] == '_')
                    continue;
                int j = Array.IndexOf(solutionCopy, guessed[i]);
                if (j > -1)
                {
                    solutionCopy[j] = '_';
                    whitePins++;
                }

            }
            return new Points(blackPins, whitePins);
        }

        public String PointsAsString(string guessedWord)
        {
            Points p = WordPoints(guessedWord);
            return String.Format(Resources.GuessedWordPointsAsString, p.blackpins, p.whitepins);
        }

        public String GetSolution() => solution;
    }
}
