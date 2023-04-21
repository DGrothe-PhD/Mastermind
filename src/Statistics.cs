namespace MastermindVariante
{
    public partial class Statistics : FormDarkMode
    {
        private readonly string name = "";
        private readonly PointsScored pointsScored;
        public Statistics()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.Manual;
            if (GuessedRow.caller != null)
                Location = GuessedRow.caller.Location.MoveBy(GuessedRow.caller.Width + 10, 0);

            pointsScored = new PointsScored();
            pointsScored.ReadData();
            txtScoredPoints.Text = pointsScored.ToString();
            txtScoredPoints.SelectionLength = 0;
            txtScoredPoints.TabStop = false;
            Focus();
        }

        public Statistics(string name) : this()
        {
            this.name = name;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            txtScoredPoints.SelectAll();
            txtScoredPoints.Copy();
            txtScoredPoints.SelectionLength = 0;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkShowFriends.Checked)
            {
                txtScoredPoints.Text = pointsScored.ToString();
            }
            else if (name != "Debug")
            {
                txtScoredPoints.Text = pointsScored.MyResult(name);
            }
        }

        internal string[] GetPlayerNames()
        {
            var names = pointsScored.Players.Select(x => x.NickName).ToArray();
            Array.Sort(names);
            return names;
        }
    }
}
