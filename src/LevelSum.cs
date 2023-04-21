namespace MastermindVariante
{
    public struct LevelSum
    {
        // wie beim Minigolf. Aufschreiben, wie viele Reihen (Rateversuche) der Spieler (m/w/d) braucht.
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
                return $" - L{level} noch nicht gespielt";
            return $" - L{level}: {rows.Sum()} Versuche in {numGames} Spielen,\r\n" +
                $"   Schnitt {(double)rows.Average():0.00}, " +
                $"Bestwert {rows.Min()}";
        }
    }
}
