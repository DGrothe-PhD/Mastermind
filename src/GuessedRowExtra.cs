using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindVariante
{
    public partial class GuessedRow
    {
        //Aufteilung der Klasse dient der Übersichtlichkeit.
        private void FormatObjects()
        {
            Point rowEnd = pieces[^1].Location;
            btnEdit.BackColor = Color.DarkSeaGreen;
            btnEdit.Location = new Point(rowEnd.X + width + paddingLeft, rowEnd.Y);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(width, height);
            btnEdit.Text = "";
            btnEdit.BackgroundImage = Image.FromFile("assets/kbd.png");
            btnEdit.BackgroundImageLayout = ImageLayout.Stretch;
            btnEdit.UseVisualStyleBackColor = false;

            btnSubmit.BackColor = Color.DarkSeaGreen;
            btnSubmit.Location = new Point(rowEnd.X + 2 * width + 2 * paddingLeft, rowEnd.Y);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(width, height);
            btnSubmit.Text = "OK";
            btnSubmit.UseVisualStyleBackColor = false;

            btnClear.BackColor = Color.IndianRed;
            btnClear.Location = new Point(rowEnd.X + 3 * width + 3 * paddingLeft, rowEnd.Y);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(width, height);
            btnClear.BackgroundImage = Image.FromFile("assets/eraser.png");
            btnClear.BackgroundImageLayout = ImageLayout.Stretch;
            btnClear.Text = "";
            btnClear.UseVisualStyleBackColor = false;

            //instanzenabhängig
            btnSubmit.Click += CalculatePoints;
            btnEdit.Click += TypeGuessedWord;
            btnClear.Click += Clear;

            caller!.Controls.Add(btnEdit);
            caller!.Controls.Add(btnSubmit);
            caller!.Controls.Add(btnClear);

            // Pinreihe zur Seite schieben
            pins.ForEach(x => x.MoveBy(3 * btnSubmit.Width + 4 * paddingLeft, 0));

        }

        private void TypeGuessedWord(object? sender, EventArgs e)
        {
            OpenKeyboard();
        }

        internal void OpenKeyboard()
        {
            Keyboard kbd = new(withTips, excludedChars ?? new List<char>());
            kbd.SetCaller(this);
            kbd.Show();
        }
    }
}
