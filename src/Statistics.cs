using Lang;

namespace MastermindVariante
{
    public partial class Statistics : FormDarkMode
    {
        private readonly string name = "";
        private readonly PointsScored pointsScored;
        public Statistics()
        {
            CurrentConfiguration.ApplyLanguage();
            InitializeComponent();

            ChkShowFriends.Font = ChkShowFriends.Font.Resize(Parameter.resizePercentage);

            StartPosition = FormStartPosition.Manual;
            if (GuessedRow.Caller != null)
                Location = GuessedRow.Caller.Location.MoveBy(GuessedRow.Caller.Width + 10, 0);

            btnCopy.Text = Resources.CopyStatisticsButtonText;
            ChkShowFriends.Text = Resources.ShowFriendsCheckBox;
            ChkShowLastWeeks.Text = Resources.ShowLastCheckBox;

            Text = Resources.StatisticsWindowTitle;

            pointsScored = new PointsScored();
            pointsScored.ReadData();

            ChkShowFriends.Checked = true;
            ChkShowLastWeeks.Checked = false;
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

        private void ChkShowFriends_CheckedChanged(object sender, EventArgs e)
        {
            ShowGameResults();
        }

        private void ShowGameResults()
        {
            if (ChkShowFriends.Checked)
            {
                if (ChkShowLastWeeks.Checked)
                {
                    txtScoredPoints.Text = pointsScored.LastResults();
                }
                else
                {
                    txtScoredPoints.Text = pointsScored.ToString();
                }
            }
            else if (name != "Debug")
            {
                txtScoredPoints.Text = pointsScored.MyResult(name, ChkShowLastWeeks.Checked);
            }
        }

        internal string[] GetPlayerNames()
        {
            var names = pointsScored.Players.Select(x => x.NickName).ToArray();
            Array.Sort(names);
            return names;
        }

        private void ChkShowLastWeeks_CheckedChanged(object sender, EventArgs e)
        {
            ShowGameResults();
        }
    }
}
