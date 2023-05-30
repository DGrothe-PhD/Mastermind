
using MastermindVariante.Properties;
using System.Windows.Forms.Automation;

namespace MastermindVariante
{
    public class Piece : Parameter, ILetters, IMMDispose
    {
        // Piece is a letter or free letter field
        private readonly short rowindex, fieldindex;
        private readonly int positionX, positionY;
        private readonly Label elm;
        private readonly Form1 caller;

        private static UIFieldChanged<Label, Object>? _changed;

        public Point Location { get => elm.Location; }
        public char? ChosenChar { get => String.IsNullOrEmpty(elm?.Text) ? null : elm?.Text[0]; }

        public Piece(Form1 caller)
        {
            elm = new Label();
            SetLetter(null);
            this.caller = caller;
            _changed = SetWoodenAppearance;
        }

        /// <summary>
        /// A label as a place for a letter on it.
        /// </summary>
        /// <param name="caller">Form instance where the label is to be placed on.</param>
        /// <param name="rowindex">Index of the row this piece belongs to.</param>
        /// <param name="fieldindex">Index of the field (0..wordlength-1) in that row.</param>
        public Piece(Form1 caller, short rowindex, short fieldindex) : this(caller)
        {
            this.rowindex = rowindex;
            this.fieldindex = fieldindex;

            this.positionX = initialPositionX + fieldindex * (width + paddingLeft);
            positionY = initialPositionY + rowindex * (height + paddingBottom);

            FormatPiece();
            caller.Controls.Add(elm);
        }

        private void FormatPiece()
        {
            elm.BorderStyle = BorderStyle.Fixed3D;
            elm.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point).Resize(resizePercentage);
            elm.BackColor = Color.Transparent;

            elm.Location = new Point(positionX, positionY);
            elm.Name = $"LetterR{rowindex}F{fieldindex}";
            elm.Size = new Size(width, height);
            elm.TextAlign = ContentAlignment.MiddleCenter;

            elm.Click += Elm_Click;
        }

        /// <summary>
        /// When user clicks on a piece, it opens the keyboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Elm_Click(object? sender, EventArgs e)
        {
            elm.BackColor = Color.FromArgb(100, 255, 255, 0);
            Keyboard kbd = new(Form1.WithTips, GuessedRow.ExcludedChars ?? new List<char>());
            kbd.SetCaller(this);
            kbd.Show();
        }

        /// <summary>
        /// Stop the piece from receiving clicks after this row has been evaluated and the next row is opened.
        /// </summary>
        public void Disable()
        {
            elm.Click -= Elm_Click;
        }

        /// <summary>
        /// If letter is selected, display that letter and change the letter field's design.
        /// </summary>
        /// <param name="letter"></param>
        public void SetLetter(char? letter)
        {
            elm.Text = letter == null ? "" : "" + letter;
            _changed?.Invoke(elm, "" + letter ?? "");
        }

        /// <summary>
        /// Resets the label to empty state.
        /// </summary>
        public void RemoveLetter()
        {
            elm.Text = "";
            _changed?.Invoke(elm, "");
        }

        public string GetLetter() => elm.Text;

        /// <summary>
        /// Dispose element(s) via the IMMDispose interface
        /// </summary>
        public void Remove()
        {
            caller.Controls.Remove(elm);
            elm.Dispose();
        }

    }

}
