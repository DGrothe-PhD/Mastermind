
namespace MastermindVariante
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            numWordLength = new NumericUpDown();
            txtUserName = new TextBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            EndGameToolStripMenuItem = new ToolStripMenuItem();
            NewGameToolStripMenuItem = new ToolStripMenuItem();
            PlayModeToolStripMenuItem = new ToolStripMenuItem();
            PlayAgainstComputerToolStripMenuItem = new ToolStripMenuItem();
            PlayWithPeerToolStripMenuItem = new ToolStripMenuItem();
            FromFileToolStripMenuItem = new ToolStripMenuItem();
            ShowStatisticsToolStripMenuItem = new ToolStripMenuItem();
            CloseWindowToolStripMenuItem = new ToolStripMenuItem();
            ShowTipsToolStripMenuItem = new ToolStripMenuItem();
            btnStart = new Button();
            CmbNames = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)numWordLength).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(12, 8);
            label1.Name = "label1";
            label1.Size = new Size(170, 35);
            label1.TabIndex = 5;
            label1.Text = "Wortratespiel";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(242, 15);
            label2.Name = "label2";
            label2.Size = new Size(95, 23);
            label2.TabIndex = 2;
            label2.Text = "Wortlänge";
            // 
            // numWordLength
            // 
            numWordLength.Location = new Point(343, 15);
            numWordLength.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numWordLength.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            numWordLength.Name = "numWordLength";
            numWordLength.Size = new Size(56, 27);
            numWordLength.TabIndex = 1;
            numWordLength.Value = new decimal(new int[] { 4, 0, 0, 0 });
            numWordLength.ValueChanged += NumericUpDown1_ValueChanged;
            // 
            // txtUserName
            // 
            txtUserName.BackColor = Color.WhiteSmoke;
            txtUserName.Location = new Point(405, 15);
            txtUserName.Multiline = true;
            txtUserName.Name = "txtUserName";
            txtUserName.PlaceholderText = "Spielername (optional)";
            txtUserName.Size = new Size(211, 30);
            txtUserName.TabIndex = 0;
            txtUserName.KeyPress += txtUserName_KeyPress;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { EndGameToolStripMenuItem, NewGameToolStripMenuItem, PlayModeToolStripMenuItem, ShowStatisticsToolStripMenuItem, CloseWindowToolStripMenuItem, ShowTipsToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(191, 148);
            // 
            // spielBeendenToolStripMenuItem
            // 
            EndGameToolStripMenuItem.Name = "spielBeendenToolStripMenuItem";
            EndGameToolStripMenuItem.Size = new Size(190, 24);
            EndGameToolStripMenuItem.Text = "Spiel b&eenden";
            EndGameToolStripMenuItem.Click += EndThisRoundToolStripMenuItem_Click;
            // 
            // neuesSpielToolStripMenuItem
            // 
            NewGameToolStripMenuItem.Name = "neuesSpielToolStripMenuItem";
            NewGameToolStripMenuItem.Size = new Size(190, 24);
            NewGameToolStripMenuItem.Text = "&Neues Spiel";
            NewGameToolStripMenuItem.Click += NewRoundToolStripMenuItem_Click;
            // 
            // spielmodusToolStripMenuItem
            // 
            PlayModeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { PlayAgainstComputerToolStripMenuItem, PlayWithPeerToolStripMenuItem, FromFileToolStripMenuItem });
            PlayModeToolStripMenuItem.Name = "spielmodusToolStripMenuItem";
            PlayModeToolStripMenuItem.Size = new Size(190, 24);
            PlayModeToolStripMenuItem.Text = "Spiel&modus";
            // 
            // gegenDenComputerToolStripMenuItem
            // 
            PlayAgainstComputerToolStripMenuItem.Name = "gegenDenComputerToolStripMenuItem";
            PlayAgainstComputerToolStripMenuItem.Size = new Size(237, 26);
            PlayAgainstComputerToolStripMenuItem.Text = "Gegen den Computer";
            // 
            // gegnerStelltAufgabeToolStripMenuItem
            // 
            PlayWithPeerToolStripMenuItem.Name = "gegnerStelltAufgabeToolStripMenuItem";
            PlayWithPeerToolStripMenuItem.Size = new Size(237, 26);
            PlayWithPeerToolStripMenuItem.Text = "Gegner stellt Aufgabe";
            PlayWithPeerToolStripMenuItem.Click += GetWordFromPeerToolStripMenuItem_Click;
            // 
            // vonDateiToolStripMenuItem
            // 
            FromFileToolStripMenuItem.Name = "vonDateiToolStripMenuItem";
            FromFileToolStripMenuItem.Size = new Size(237, 26);
            FromFileToolStripMenuItem.Text = "Von Datei...";
            FromFileToolStripMenuItem.Click += FromFileToolStripMenuItem_Click;
            // 
            // spielstatistikF4ToolStripMenuItem
            // 
            ShowStatisticsToolStripMenuItem.Name = "spielstatistikF4ToolStripMenuItem";
            ShowStatisticsToolStripMenuItem.Size = new Size(190, 24);
            ShowStatisticsToolStripMenuItem.Text = "S&pielstatistik... F4";
            ShowStatisticsToolStripMenuItem.Click += ShowStatisticsToolStripMenuItem_Click;
            // 
            // fensterSchliessenToolStripMenuItem
            // 
            CloseWindowToolStripMenuItem.Name = "fensterSchließenToolStripMenuItem";
            CloseWindowToolStripMenuItem.Size = new Size(190, 24);
            CloseWindowToolStripMenuItem.Text = "Fenster s&chließen";
            CloseWindowToolStripMenuItem.Click += CloseWindowToolStripMenuItem_Click;
            // 
            // tipsToolStripMenuItem
            // 
            ShowTipsToolStripMenuItem.CheckOnClick = true;
            ShowTipsToolStripMenuItem.Name = "tipsToolStripMenuItem";
            ShowTipsToolStripMenuItem.Size = new Size(190, 24);
            ShowTipsToolStripMenuItem.Text = "Tipps";
            ShowTipsToolStripMenuItem.Click += tipsToolStripMenuItem_Click;
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.Peru;
            btnStart.Location = new Point(188, 12);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(48, 30);
            btnStart.TabIndex = 6;
            btnStart.Text = "Neu";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += BtnStart_Click;
            // 
            // CmbNames
            // 
            CmbNames.FormattingEnabled = true;
            CmbNames.Location = new Point(404, 60);
            CmbNames.Name = "CmbNames";
            CmbNames.Size = new Size(213, 28);
            CmbNames.TabIndex = 7;
            CmbNames.SelectedIndexChanged += CmbNames_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackgroundImage = Properties.Resources.background;
            ClientSize = new Size(628, 718);
            ContextMenuStrip = contextMenuStrip1;
            Controls.Add(CmbNames);
            Controls.Add(btnStart);
            Controls.Add(txtUserName);
            Controls.Add(numWordLength);
            Controls.Add(label2);
            Controls.Add(label1);
            Location = new Point(0, 0);
            Name = "Form1";
            Text = "MasterMind 1.0";
            FormClosing += Form1_FormClosing;
            KeyDown += Form1_KeyDown;
            KeyPress += Form1_KeyPress;
            ((System.ComponentModel.ISupportInitialize)numWordLength).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private NumericUpDown numWordLength;
        public TextBox txtUserName;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem EndGameToolStripMenuItem;
        private ToolStripMenuItem NewGameToolStripMenuItem;
        private ToolStripMenuItem PlayModeToolStripMenuItem;
        private ToolStripMenuItem PlayAgainstComputerToolStripMenuItem;
        private ToolStripMenuItem PlayWithPeerToolStripMenuItem;
        private ToolStripMenuItem FromFileToolStripMenuItem;
        private ToolStripMenuItem ShowStatisticsToolStripMenuItem;
        private ToolStripMenuItem CloseWindowToolStripMenuItem;
        private Button btnStart;
        private ComboBox CmbNames;
        private ToolStripMenuItem ShowTipsToolStripMenuItem;
    }
}