namespace MastermindVariante
{
    public partial class GuessedRow : Parameter, ILetters, IMMDispose
    {
        private static short rowIndex = -1;

        internal static Form1? Caller { get; private set; }
        private readonly List<Piece> pieces;
        private readonly List<ResultPin> pins;
        internal Button btnSubmit, btnClear, btnEdit;
        private Points result;
        public Points Result { get => result; }
        public static int NumberOfRows { get => (rowIndex + 1); }

        internal static List<char>? ExcludedChars { get; private set; }

        public GuessedRow(Form1 _caller)
        {
            Caller = _caller;

            btnSubmit = new();
            btnClear = new();
            btnEdit = new();

            if (rowIndex < 0)
                ExcludedChars = new List<char>();

            rowIndex++;
            short level = Caller?.WordLength ?? 4;

            pieces = new();
            pins = new();

            for (short i = 0; i < level; i++)
            {
                pieces.Add(new Piece(Caller!, rowIndex, i));
                pins.Add(new ResultPin(Caller!, rowIndex, i));
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
                Caller?.NextRow();
        }

        internal bool Calculate()
        {
            // With empty fields, row is not evaluated
            if (pieces.Any(x => String.IsNullOrEmpty(x.GetLetter())))
                return false;

            result = Caller!.Evaluation.WordPoints(this.ToString());

            pins.Take(result.blackpins).ToList().ForEach(x => x.MakeBlack());
            pins.Skip(result.blackpins).Take(result.whitepins).ToList().ForEach(x => x.MakeWhite());

            // For tip function: Collect letters of words with zero points - Buttons are hidden next time
            if (result.blackpins == 0 && result.whitepins == 0)
            {
                foreach (var x in pieces)
                {
                    if (!ExcludedChars!.Contains(x.GetLetter()[0]))
                    {
                        ExcludedChars.Add(x.GetLetter()[0]);
                    }
                }
            }

            pieces.ForEach(x => x.Disable());

            HideButtons();

            // full black row == match.
            Caller.GameIsRunning = !(result.Equals(new Points(Caller.WordLength, 0)));

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
