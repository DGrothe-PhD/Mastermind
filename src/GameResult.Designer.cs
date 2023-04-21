namespace MastermindVariante
{
    partial class GameResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameResult));
            lblResult = new Label();
            pnlEmoji = new Panel();
            lblExtraInfo = new Label();
            SuspendLayout();
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.BackColor = Color.Transparent;
            lblResult.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            lblResult.ForeColor = Color.Gold;
            lblResult.Location = new Point(193, 13);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(41, 38);
            lblResult.TabIndex = 0;
            lblResult.Text = "...";
            // 
            // pnlEmoji
            // 
            pnlEmoji.Location = new Point(12, 13);
            pnlEmoji.Name = "pnlEmoji";
            pnlEmoji.Size = new Size(156, 161);
            pnlEmoji.TabIndex = 1;
            // 
            // lblExtraInfo
            // 
            lblExtraInfo.AutoSize = true;
            lblExtraInfo.BackColor = Color.Transparent;
            lblExtraInfo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblExtraInfo.ForeColor = Color.Chocolate;
            lblExtraInfo.Location = new Point(193, 127);
            lblExtraInfo.Name = "lblExtraInfo";
            lblExtraInfo.Size = new Size(70, 28);
            lblExtraInfo.TabIndex = 2;
            lblExtraInfo.Text = "label1";
            // 
            // GameResult
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(598, 205);
            Controls.Add(lblExtraInfo);
            Controls.Add(pnlEmoji);
            Controls.Add(lblResult);
            Location = new Point(0, 0);
            Name = "GameResult";
            Text = "GameResult";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblResult;
        private Panel pnlEmoji;
        private Label lblExtraInfo;
    }
}