using Lang;
using System.Threading.Tasks.Dataflow;

namespace MastermindVariante
{
    public struct LevelSum
    {
        // counting guessings taken.
        private int level;
        public int Level { get => level; set => level = value; }
        private readonly List<int> rows;
        private int numGames;

        public LevelSum(int level, int rows, int numGames = 1)
        {
            this.level = level;
            this.rows = new() { rows };
            this.numGames = numGames;
        }

        /// <summary>
        /// Add a number of rows of a game for that level.
        /// </summary>
        /// <param name="rows">Number of rows to guess a word in a game</param>
        /// <returns></returns>
        public LevelSum Add(int rows)
        {
            this.numGames++;
            this.rows.Add(rows);
            return this;
        }

        public List<int> Filter(int lastHowMany = 3)
        {
            return rows.TakeLast(lastHowMany).ToList();
        }

        public string LastResults()
        {
            return $"{Resources.YourLastResults} {level}: "+ String.Join(", ", Filter());
        }

        public override string ToString()
        {
            if (numGames == 0)
                return String.Format(" - "+ Resources.LevelNotPlayedYet, level);
            return String.Format($" - {level}: " + Resources.NTriesInMGames + Environment.NewLine,
                    rows.Sum(), numGames) +
                String.Format("   "+ Resources.AverageAndBest, $"{(double)rows.Average():0.00}", rows.Min());
        }
    }
}
