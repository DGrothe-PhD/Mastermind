using System.Text;

namespace MastermindVariante
{
    internal class Player
    {
        public string NickName { get; set; }
        public List<LevelSum> Results { get; set; }

        public Player(string NickName)
        {
            this.NickName = NickName;
            Results = new();
        }

        public void AddResult(int level, int rows)
        {
            int found = Results.FindIndex(x => x.Level == level);
            if (found > -1)
            {
                var item = Results[found];
                Results[found] = item.Add(rows);
            }
            else
            {
                Results.Add(new LevelSum(level, rows));
            }
        }

        public string LastResults()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(NickName + ":");
            Results.ForEach(Results => sb.AppendLine(Results.LastResults()));
            return sb.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine(NickName + ":");
            Results.ForEach(Results => sb.AppendLine(Results.ToString()));
            return sb.ToString();
        }
    }
}
