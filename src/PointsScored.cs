using System.Text;

namespace MastermindVariante
{
    public class PointsScored
    {
        private readonly string dateiname = "Resources/gameresults.csv";
        private string[]? lines;
        internal readonly List<Player> Players;

        public PointsScored()
        {
            Players = new List<Player>();
        }

        public void ReadData()
        {
            try
            {
                Players.Clear();
                lines = File.ReadAllLines(dateiname);

                //csv has header
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(';');
                    int k = Players.FindIndex(x => x.NickName == parts[0]);

                    if (k == -1)
                    {
                        Players.Add(new Player(parts[0]));
                        k = Players.Count - 1;
                    }

                    if (Int32.TryParse(parts[2], out int level) && Int32.TryParse(parts[3], out int rows))
                        Players[k].AddResult(level, rows);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            foreach (Player player in Players)
            {
                sb.AppendLine(player.ToString());
            }

            return sb.ToString();
        }

        public string MyResult(string name)
        {
            return Players?.FirstOrDefault(x => x.NickName == name)?.ToString() ?? "";
        }
    }
}
