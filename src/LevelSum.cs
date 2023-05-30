using Lang;

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

        public LevelSum Add(int rows)
        {
            this.numGames++;
            this.rows.Add(rows);
            return this;
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
