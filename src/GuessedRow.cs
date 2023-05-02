using System.Diagnostics.Metrics;

namespace MastermindVariante
{
    public partial class GuessedRow : Parameter, ILetters, IMMDispose
    {
        private static short rowIndex = -1;
        internal static Form1? caller { get; private set; }
        private readonly List<Piece> pieces;
        private readonly List<ResultPin> pins;
        internal Button btnSubmit, btnClear, btnEdit;
        private Points result;
        public Points Result { get => result; }
        public static int NumberOfRows { get => (rowIndex + 1); }

        internal static List<char>? excludedChars { get; private set; }

        public GuessedRow(Form1 _caller)
        {
            caller = _caller;

            btnSubmit = new();
            btnClear = new();
            btnEdit = new();

            if (rowIndex < 0)
                excludedChars = new List<char>();

            rowIndex++;
            short level = caller?.WordLength ?? 4;

            pieces = new();
            pins = new();

            for (short i = 0; i < level; i++)
            {
                pieces.Add(new Piece(caller, rowIndex, i));
                pins.Add(new ResultPin(caller, rowIndex, i));
            }

            FormatObjects();
        }


        public void Clear(object? sender, EventArgs e)
        {
            pieces.ForEach(x => x.SetLetter(null));
            pins.ForEach(x => x.MakeEmpty());
        }

        public void SetLetter(char? letter)
        {
            //Automatisches Anfügen eines neuen Buchstabens.
            // Für die Zufallsautomatik beim Testen verwendet; später für eine Tastatureingabefunktion sinnvoll.
            for (short i = 0; i < pieces.Count; i++)
            {
                if (pieces[i].GetLetter() == "")
                {
                    pieces[i].SetLetter(letter);
                    break;
                }
            }
        }

        public void RemoveLetter()
        {
            for (short i = (short)(pieces.Count - 1); i >= 0; i--)
            {
                if (pieces[i].GetLetter() != "")
                {
                    pieces[i].SetLetter(null);
                    break;
                }
            }
        }

        public void Remove()
        {
            pieces.ForEach(x => x.Remove());
            pieces.Clear();

            pins.ForEach(x => x.Remove());
            pins.Clear();

            btnClear?.Dispose();
            btnSubmit?.Dispose();
            btnEdit?.Dispose();
            rowIndex--;
        }

        public void HideButtons()
        {
            btnClear?.Hide();
            btnSubmit?.Hide();
            btnEdit?.Hide();
        }


        private void CalculatePoints(object? sender, EventArgs e)
        {
            if (Calculate())
                caller?.NextRow();
        }

        internal bool Calculate()
        {
            // Leere Buchstabenfelder: Wort nicht auswerten
            if (pieces.Any(x => String.IsNullOrEmpty(x.GetLetter())))
                return false;

            result = caller!.Evaluation.WordPoints(this.ToString());

            pins.Take(result.blackpins).ToList().ForEach(x => x.MakeBlack());
            pins.Skip(result.blackpins).Take(result.whitepins).ToList().ForEach(x => x.MakeWhite());

            // Nutzer legt Wort hin, dessen Buchstaben keine Treffer haben.
            // Die Tastatur kann diese fortan ausblenden als kleine Hilfe.
            if (result.blackpins == 0 && result.whitepins == 0)
            {
                foreach (var x in pieces)
                {
                    if (!(excludedChars!.Contains(x.GetLetter()[0])))
                    {
                        excludedChars.Add(x.GetLetter()[0]);
                    }
                }
            }

            pieces.ForEach(x => x.Disable());

            HideButtons();

            // full black row == match.
            caller.GameIsRunning = !(result.Equals(new Points(caller.WordLength, 0)));

            return true;
        }

        public override string ToString()
        {
            string guessedWord = string.Empty;
            foreach (var piece in pieces)
            {
                guessedWord += piece.ChosenChar;
            }
            return guessedWord;
        }
    }

}
