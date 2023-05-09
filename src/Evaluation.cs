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
                3 => WordLists.WordsWith3.Pick(),
                5 => WordLists.WordsWith5.Pick(),
                _ => WordLists.WordsWith4.Pick(),
            };
        }

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
