using Lang;

namespace MastermindVariante
{
    public partial class GameResult : FormDarkMode
    {
        // Form to show when user has won.
        private readonly string name;
        private readonly int numberOfRows;
        private readonly short level;
        private FileStream? fs;
        private StreamWriter? sw;
        public GameResult(string name, short level, int numberOfRows)
        {
            CurrentConfiguration.ApplyLanguage();
            this.name = name;
            this.level = level;
            this.numberOfRows = numberOfRows;
            this.Text = Resources.GameResultTitle;

            InitializeComponent();
            FormatDialog();
            StoreResult();
            StartPosition = FormStartPosition.Manual;
            if (GuessedRow.Caller != null)
                Location = GuessedRow.Caller.Location.MoveBy(10, (int)GuessedRow.Caller.Height / 5);
        }


        private void FormatDialog()
        {
            Size = Size.Rescale(Parameter.resizePercentage);

            MaximizeBox = false;
            MaximumSize = Size;
            MinimumSize = Size;

            pnlEmoji.BackgroundImage = Properties.Resources.won;
            pnlEmoji.BackgroundImageLayout = ImageLayout.Stretch;
            pnlEmoji.Size = pnlEmoji.Size.Rescale(Parameter.resizePercentage);

            lblResult.Text = name + Environment.NewLine + Resources.YouHaveWon;
            lblResult.Location = new Point(
                pnlEmoji.Width + 24 * Parameter.resizePercentage / 100,
                lblResult.Location.Y
            );
            lblExtraInfo.Location = new Point(
                pnlEmoji.Width + 24 * Parameter.resizePercentage / 100,
                lblResult.Height + lblResult.Location.Y + 24 * Parameter.resizePercentage / 100
            );

            Size = new Size(Width, pnlEmoji.Size.Height + 28 * Parameter.resizePercentage/100);

            lblExtraInfo.Text = String.Format(Resources.TellNumberOfRows, numberOfRows);

            lblExtraInfo.Font = lblExtraInfo.Font.Resize(Parameter.resizePercentage);
            lblResult.Font = lblResult.Font.Resize(Parameter.resizePercentage);
        }

        private void StoreResult()
        {
            //Debug games are not stored.
            if (name == "Debug")
                return;
            try
            {
                fs = new FileStream("Resources/gameresults.csv", FileMode.Append);
                sw = new StreamWriter(fs);
                sw.WriteLine(String.Join(';', name, DateTime.Today.ToString("d"), level, numberOfRows));
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
