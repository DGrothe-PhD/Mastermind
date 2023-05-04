
using System.Configuration;
using System.Diagnostics;

using Lang;

namespace MastermindVariante
{
    public partial class Form1 : FormDarkMode
    {
        bool isRunning = false;
        public static bool WithTips { get; private set; }

        string name = "";
        public Evaluation Evaluation { get; private set; }
        public bool GameIsRunning { get => isRunning; set => isRunning = value; }

        private short wordLength;
        public short WordLength { get => wordLength; set => wordLength = value; }

        private readonly List<GuessedRow> rows;

        public Form1()
        {
            CurrentConfiguration.ApplyLanguage();
            InitializeComponent();
            SetElements();

            CmbNames.Show();
            GetNames();

            txtUserName.PlaceholderText = Resources.PlayerNamePlaceholder;
            txtUserName.Multiline = false;
            txtUserName.Text = "";
            txtUserName.Enabled = true;

            numWordLength.Enabled = true;

            KeyPreview = true;


            // DarkMode using this:
            // https://stackoverflow.com/questions/57124243/winforms-dark-title-bar-on-windows-10
            //
            rows = new List<GuessedRow>();
            Evaluation = new Evaluation(this);

        }

        private void SetElements()
        {
            label1 = new Label();
            label2 = new Label();

            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(12, 8);
            label1.Name = "label1";
            label1.Size = new Size(170, 35);
            label1.TabIndex = 5;
            label1.Text = Resources.DescriptiveTitle;

            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(242, 15);
            label2.Name = "label2";
            label2.Size = new Size(95, 23);
            label2.TabIndex = 2;
            label2.Text = Resources.WordLengthLabelText;

            Controls.Add(label1);
            Controls.Add(label2);

            btnStart.Text = Resources.NewGameButtonText;
        }

        private void RunNewGame()
        {
            try
            {
                DefineName();

                wordLength = (short)numWordLength.Value;
                Evaluation.SetSolution();

                foreach (GuessedRow row in rows)
                {
                    row.Remove();
                }
                rows.Clear();

                numWordLength.Enabled = false;
                txtUserName.Enabled = false;
                CmbNames.Hide();
                GameIsRunning = true;

                if (Debugger.IsAttached)
                    MessageBox.Show(String.Format(Resources.DebugShowSolution, Evaluation.GetSolution()));

                NewRow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorMessageTitle);
            }
        }

        private void DefineName()
        {
            if (Debugger.IsAttached)
            {
                // Debugger nicht in Statistik
                name = "Debug";
                txtUserName.Text = "Debug";
            }
            else if (txtUserName.Text.Trim().Length > 2)
                name = txtUserName.Text;
            else
                name = "Anonymous Koala";
        }

        private void NewRow() => rows.Add(new GuessedRow(this));

        private void FinishGame(bool hasWon = false)
        {
            rows.LastOrDefault()?.HideButtons();
            numWordLength.Enabled = true;
            txtUserName.Enabled = true;

            GetNames();
            CmbNames.Show();

            if (!hasWon) MessageBox.Show(String.Format(Resources.GameEndedMessage, Evaluation.GetSolution()));
            GameIsRunning = false;
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            wordLength = (short)numWordLength.Value;
            RunNewGame();
        }

        internal void NextRow()
        {
            if (GameIsRunning)
            {
                NewRow();
            }
            else
            {
                GameResult gameResult = new(name, wordLength, GuessedRow.NumberOfRows);
                gameResult.ShowDialog();
                FinishGame(true);
            }
        }

        private void CloseWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GameIsRunning)
            {
                MessageBox.Show(Resources.RunningGameMessageText, Resources.RunningGameMessageTitle);
                return;
            }
            Close();
        }

        private static void Undercons() => MessageBox.Show(Resources.FutureFeatureMessageText, Resources.FutureFeatureMessageTitle);

        private void ShowStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GameIsRunning)
                ShowStatistics();
        }

        private void ShowStatistics()
        {
            DefineName();
            Statistics stat = new(name);
            stat.Show();
        }

        private void GetNames()
        {
            Statistics stat = new();
            CmbNames.Items.Clear();
            CmbNames.Items.AddRange(stat.GetPlayerNames());
        }

        private void GetWordFromPeerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undercons();
        }

        private void FromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undercons();
        }

        private void NewRoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GameIsRunning)
            {
                if (ConfirmEnd() == DialogResult.Yes)
                {
                    FinishGame();
                    RunNewGame();
                }
            }
            else
            {
                RunNewGame();
            }
        }

        private void EndThisRoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!GameIsRunning)
                MessageBox.Show(Resources.NoRunningGameText, Resources.NoRunningGameTitle);
            else if (ConfirmEnd() == DialogResult.Yes)
                FinishGame();
        }

        private static DialogResult ConfirmEnd()
        {
            //[tsl] GivingUpQuestion, GivingUpTitle
            return MessageBox.Show(Resources.GivingUpQuestion,
                Resources.GivingUpTitle,
                MessageBoxButtons.YesNo);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GameIsRunning)
                e.Cancel = (ConfirmEnd() != DialogResult.Yes);
        }

        #region KeyEvents
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 && GameIsRunning)
            {
                rows.Last().OpenKeyboard();
            }
            else if (e.KeyCode == Keys.F1)
            {
                //[tsl] QuickHelpText
                MessageBox.Show("F2 öffnet Tastatur,\r\n# = Okay/Wort auswerten\r\nF4 öffnet Statistik");
            }
            else if (e.KeyCode == Keys.F4 && !GameIsRunning)
            {
                ShowStatistics();
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '#' && GameIsRunning)
            {
                e.Handled = true;
                if (rows.Last().Calculate())
                    NextRow();
            }
        }
        private void TxtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ignore line breaks
            if (e.KeyChar == (char)0x0d || e.KeyChar == (char)0x0a)
                e.Handled = true;
            //if (!Char.IsLetterOrDigit(e.KeyChar)) e.Handled = true;
        }
        #endregion

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (GameIsRunning)
            {
                if (ConfirmEnd() == DialogResult.Yes)
                {
                    FinishGame();
                    RunNewGame();
                }
            }
            else
            {
                RunNewGame();
            }
        }

        private void CmbNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtUserName.Text = CmbNames.SelectedItem?.ToString() ?? "";
        }

        private void TipsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WithTips = ShowTipsToolStripMenuItem.Checked;
        }

        bool currently = false;
        private void LanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!currently)
            {
                currently = true;
                MessageBox.Show("Switch to English");
                CurrentConfiguration.SetLanguage("en-GB");
                CurrentConfiguration.ApplyLanguage();
            }
            else
            {
                currently = false;
                MessageBox.Show("Switch to German");
                CurrentConfiguration.SetLanguage("de-DE");
                CurrentConfiguration.ApplyLanguage();
            }
        }
    }
}