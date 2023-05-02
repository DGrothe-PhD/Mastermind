namespace MastermindVariante
{
    partial class Statistics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtScoredPoints = new TextBox();
            btnCopy = new Button();
            ChkShowFriends = new CheckBox();
            SuspendLayout();
            // 
            // txtScoredPoints
            // 
            txtScoredPoints.BackColor = Color.White;
            txtScoredPoints.Location = new Point(26, 27);
            txtScoredPoints.Multiline = true;
            txtScoredPoints.Name = "txtScoredPoints";
            txtScoredPoints.ReadOnly = true;
            txtScoredPoints.ScrollBars = ScrollBars.Vertical;
            txtScoredPoints.Size = new Size(369, 391);
            txtScoredPoints.TabIndex = 0;
            // 
            // btnCopy
            // 
            btnCopy.BackColor = Color.Peru;
            btnCopy.Location = new Point(485, 27);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(171, 57);
            btnCopy.TabIndex = 1;
            btnCopy.Text = "Kopiere Statistik";
            btnCopy.UseVisualStyleBackColor = false;
            btnCopy.Click += Button1_Click;
            // 
            // ChkShowFriends
            // 
            ChkShowFriends.AutoSize = true;
            ChkShowFriends.BackColor = Color.Transparent;
            ChkShowFriends.Checked = true;
            ChkShowFriends.CheckState = CheckState.Checked;
            ChkShowFriends.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            ChkShowFriends.Location = new Point(485, 109);
            ChkShowFriends.Name = "ChkShowFriends";
            ChkShowFriends.Size = new Size(154, 29);
            ChkShowFriends.TabIndex = 2;
            ChkShowFriends.Text = "Freunde zeigen";
            ChkShowFriends.UseVisualStyleBackColor = false;
            ChkShowFriends.CheckedChanged += CheckBox1_CheckedChanged;
            // 
            // Statistics
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.IndianRed;
            BackgroundImage = Properties.Resources.board;
            ClientSize = new Size(800, 450);
            Controls.Add(ChkShowFriends);
            Controls.Add(btnCopy);
            Controls.Add(txtScoredPoints);
            Location = new Point(0, 0);
            Name = "Statistics";
            Text = "Statistics";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtScoredPoints;
        private Button btnCopy;
        private CheckBox ChkShowFriends;
    }
}