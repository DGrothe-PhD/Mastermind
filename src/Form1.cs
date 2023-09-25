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
            Evaluation = new Evaluation(this);
            CurrentConfiguration.Caller = this;
            CurrentConfiguration.SetInitialLanguage();
            InitializeComponent();
            SetElements();

            CmbNames.Show();
            GetNames();

            numWordLength.Enabled = true;

            KeyPreview = true;


            // DarkMode using this:
            // https://stackoverflow.com/questions/57124243/winforms-dark-title-bar-on-windows-10
            //
            rows = new List<GuessedRow>();
        }

        private void SetElements()
        {
            Size = Size.Rescale(Parameter.resizePercentage);
            btnStart.Location = new Point(252, Parameter.DefaultTopMargin);
            btnStart.Size = new Size(78, Parameter.DefaultElementHeight);

            //txtUserName.Size = txtUserName.Size.Rescale(Parameter.resizePercentage);
            //CmbNames.Size = CmbNames.Size.Rescale(Parameter.resizePercentage);

            lblTitle = new Label();
            lblWordLength = new Label();

            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point).Resize(Parameter.resizePercentage);
            lblTitle.Location = new Point(12, Parameter.DefaultTopMargin);
            lblTitle.Name = "label1";
            lblTitle.AutoSize = true;
            lblTitle.Size = new Size(140, Parameter.DefaultElementHeight);
            lblTitle.TabIndex = 5;

            lblWordLength.AutoSize = false;
            lblWordLength.BackColor = Color.Transparent;
            lblWordLength.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            lblWordLength.Name = "label2";
            lblWordLength.Size = new Size(95, 23);
            lblWordLength.TabIndex = 2;

            txtUserName.Multiline = false;
            txtUserName.Text = "";
            txtUserName.Enabled = true;

            PositionElements();

            Controls.Add(lblTitle);
            Controls.Add(lblWordLength);

            LanguageToolStripMenuItem.DropDownItems.AddRange(CurrentConfiguration.GetToolStrips<ToolStripMenuItem>());

            foreach (ToolStripMenuItem ltm in LanguageToolStripMenuItem.DropDownItems)
            {
                ltm.Click += Language_Click;
            }

            SetElementNames();
        }

        private void PositionElements()
        {
            var currentX = lblTitle.Location.X + lblTitle.Width + Parameter.paddingLeft;
            var currentY = Parameter.DefaultTopMargin;
            btnStart.Location = new Point(currentX, currentY);
            currentX += btnStart.Width + 2 * Parameter.paddingLeft;
            lblWordLength.Location = new Point(currentX, 15);
            numWordLength.Location = new Point(currentX + lblWordLength.Width + Parameter.paddingLeft, currentY);
            currentY += Parameter.DefaultElementHeight + Parameter.paddingBottom;
            txtUserName.Location = new Point(currentX, currentY);

            currentY += Parameter.DefaultElementHeight + Parameter.paddingBottom;
            CmbNames.Location = new Point(currentX, currentY);
        }

        private void SetElementNames()
        {
            lblTitle.Text = Resources.DescriptiveTitle;
            lblWordLength.Text = Resources.WordLengthLabelText;
            btnStart.Text = Resources.NewGameButtonText;

            txtUserName.PlaceholderText = Resources.PlayerNamePlaceholder;
            CloseWindowToolStripMenuItem.Text = Resources.CloseWindowToolStripMenuItemText;
            EndGameToolStripMenuItem.Text = Resources.EndGameToolStripMenuItemText;
            FromFileToolStripMenuItem.Text = Resources.FromFileToolStripMenuItemText;
            LanguageToolStripMenuItem.Text = Resources.LanguageToolStripMenuItemText;
            NewGameToolStripMenuItem.Text = Resources.NewGameToolStripMenuItemText;
            PlayAgainstComputerToolStripMenuItem.Text = Resources.PlayAgainstComputerToolStripMenuItemText;
            PlayModeToolStripMenuItem.Text = Resources.PlayModeToolStripMenuItemText;
            PlayWithPeerToolStripMenuItem.Text = Resources.PlayWithPeerToolStripMenuItemText;
            ShowStatisticsToolStripMenuItem.Text = Resources.ShowStatisticsToolStripMenuItemText;
            ShowTipsToolStripMenuItem.Text = Resources.ShowTipsToolStripMenuItemText;

            PositionElements();
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

                txtUserName.Hide();
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
            txtUserName.Show();

            GetNames();
            CmbNames.Show();

            if (!hasWon) MessageBox.Show(Resources.GameEndedMessage + Environment.NewLine + Evaluation.GetSolution());
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
            ConfirmEndDialog();
        }

        private void ConfirmEndDialog(bool closing=false)
        {
            if(GameIsRunning && ConfirmEnd() == DialogResult.Yes)
            {
                FinishGame();
                return;
            }
            if (!closing)
            {
                MessageBox.Show(Resources.NoRunningGameText, Resources.NoRunningGameTitle);
            }
        }

        private static DialogResult ConfirmEnd()
        {
            return MessageBox.Show(Resources.GivingUpQuestion,
                Resources.GivingUpTitle,
                MessageBoxButtons.YesNo);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfirmEndDialog(true);
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
                MessageBox.Show(Resources.QuickHelpText);
            }
            else if (e.KeyCode == Keys.F4 && !GameIsRunning)
            {
                ShowStatistics();
            }
            else if (e.KeyCode == Keys.F3)
            {
                ToggleTips();
            }
        }

        private void ToggleTips()
        {
            ShowTipsToolStripMenuItem.Checked = !(ShowTipsToolStripMenuItem.Checked);
            WithTips = !WithTips;
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

        private void Language_Click(object? sender, EventArgs e)
        {
            SetElementNames();
        }

    }
}