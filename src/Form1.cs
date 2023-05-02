
using System.Diagnostics;

namespace MastermindVariante
{
    public partial class Form1 : FormDarkMode
    {
        bool isRunning = false;
        public static bool WithTips { get; private set; }

        string name = "";
        public Evaluation Evaluation { get; private set; }
        //public Keyboard? kbd;
        public bool GameIsRunning { get => isRunning; set => isRunning = value; }

        private short wordLength;
        public short WordLength { get => wordLength; set => wordLength = value; }

        private readonly List<GuessedRow> rows;

        public Form1()
        {
            InitializeComponent();

            CmbNames.Show();
            GetNames();

            txtUserName.PlaceholderText = "Spielername (optional)";
            txtUserName.Multiline = false;
            txtUserName.Text = "";
            txtUserName.Enabled = true;

            numWordLength.Enabled = true;

            KeyPreview = true;


            // DarkMode-Einstellung übernommen von
            // https://stackoverflow.com/questions/57124243/winforms-dark-title-bar-on-windows-10
            //
            rows = new List<GuessedRow>();
            Evaluation = new Evaluation(this);

        }

        private void RunNewGame()
        {
            try
            {
                DefineName();

                wordLength = (short)numWordLength.Value;
                Evaluation.SetSolution();

                // Versuch 1, die Felder schneller abzuräumen
                //foreach(Control c in Controls.OfType<Label>()
                //    .Where(x => x.Name.StartsWith("Letter")))
                //{
                //    Controls.Remove(c);
                //}
                // Versuch 2, die Felder schneller abzuräumen
                //Controls.Clear();
                //Refresh();
                //Controls.Add(label2);
                //Controls.Add(label1);
                //Controls.Add(btnStart);
                //Controls.Add(numWordLength);
                //Controls.Add(txtUserName);
                //Controls.Add(CmbNames);

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
                    MessageBox.Show("Lösung ist " + Evaluation.GetSolution());

                NewRow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler");
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

            if (!hasWon) MessageBox.Show("Spiel beendet.\r\nLösung war:\r\n" + Evaluation.GetSolution());
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
                MessageBox.Show("Bitte beenden Sie zunächst das laufende Spiel.", "Spiel");
                return;
            }
            Close();
        }

        private static void Undercons() => MessageBox.Show("Not ready", "Working on it");

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
                MessageBox.Show("Es läuft gerade kein Spiel.", "Spiel");
            else if (ConfirmEnd() == DialogResult.Yes)
                FinishGame();
        }

        private static DialogResult ConfirmEnd()
        {
            return MessageBox.Show("Wollen Sie aufgeben?", "Spiel abbrechen", MessageBoxButtons.YesNo);
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
                MessageBox.Show("F2 öffnet Tastatur,\r\n" +
                    "# = Okay/Wort auswerten");
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
        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //no new line
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

        private void tipsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WithTips = ShowTipsToolStripMenuItem.Checked;
        }
    }
}