using Lang;

namespace MastermindVariante
{
    public struct Points : IComparable<Points>
    {
        public short blackpins { get; init; }
        public short whitepins { get; init; }

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
        private WordLists wordLists;

        public Evaluation(Form1 caller)
        {
            this.caller = caller;
            wordLength = caller.WordLength;
            wordLists = new WordLists();
        }

        internal void SetWordLists(WordLists? wordLists)
        {
            if (wordLists != null)
            {
                this.wordLists = wordLists;
            }
            else
            {
                MessageBox.Show("[Info] Word lists will be empty...");
            }
        }

        public void SetSolution()
        {
            try
            {
                wordLength = caller.WordLength;

                solution = wordLength switch
                {
                    3 => wordLists!.WordsWith3!.Pick(),
                    5 => wordLists!.WordsWith5!.Pick(),
                    _ => wordLists!.WordsWith4!.Pick(),
                };
                if (String.IsNullOrEmpty(solution)){
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error - Empty word or empty word lists.");
            }
        }

        /// <summary>
        /// Let user choose a word you can guess.
        /// </summary>
        /// <param name="userChoice">given word to guess</param>
        public void SetSolution(string userChoice)
        {
            wordLength = (short)userChoice.Length;
            solution = userChoice;
        }

        /// <summary>
        /// Evaluation algorithm for Mastermind
        /// </summary>
        /// <param name="guessedWord">Guessing</param>
        /// <returns>Points(short blackpins, short whitepins)</returns>
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

        /// <summary>
        /// Provides an answer string as evaluation for a guessed word
        /// </summary>
        /// <param name="guessedWord"></param>
        /// <returns>Exact position: {# blackpins}, included: {# whitepins}</returns>
        public String PointsAsString(string guessedWord)
        {
            Points p = WordPoints(guessedWord);
            return String.Format(Resources.GuessedWordPointsAsString, p.blackpins, p.whitepins);
        }

        public String GetSolution() => solution;
    }
}
