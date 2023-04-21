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
            this.name = name;
            this.level = level;
            this.numberOfRows = numberOfRows;

            InitializeComponent();
            FormatDialog();
            StoreResult();
        }

        private void FormatDialog()
        {
            MaximizeBox = false;
            MaximumSize = Size;
            MinimumSize = Size;

            pnlEmoji.BackgroundImage = Image.FromFile("assets/won.png");
            pnlEmoji.BackgroundImageLayout = ImageLayout.Stretch;

            lblResult.Text = name + ",\r\nSie haben gewonnen!";
            lblExtraInfo.Text = $"Sie haben {numberOfRows} Reihen gebraucht.";
        }

        private void StoreResult()
        {
            //Debug games are not stored.
            if (name == "Debug")
                return;
            try
            {
                fs = new FileStream("gameresults.csv", FileMode.Append);
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
