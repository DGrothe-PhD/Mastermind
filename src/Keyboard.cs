namespace MastermindVariante
{
    public delegate void typingHandler(Button? typed);
    public delegate void BackspaceHandler();
    public partial class Keyboard : FormDarkMode
    {
        readonly int InitialPositionX, InitialPositionY;
        readonly List<Button> buttons;
        private readonly List<char> excluded;

        readonly bool withTips = false;

        //related elements
        Button? current_representative;
        GuessedRow? callingRow;
        Piece? callingPiece;

        EventHandler? adaptedClick;
        typingHandler? typingHandler;
        BackspaceHandler? backspaceHandler;

        public Keyboard()
        {
            InitializeComponent();

            //do not let user resize this form
            MaximizeBox = false;
            MaximumSize = Size;
            MinimumSize = Size;

            callingPiece = null;
            callingRow = null;
            typingHandler = null;
            backspaceHandler = null;

            InitialPositionX = Parameter.initialPositionX;
            InitialPositionY = Parameter.initialPositionY;

            KeyPreview = true;
            buttons = new();
            excluded = new List<char>();
        }

        public Keyboard(bool withTips, List<char> excluded) : this()
        {
            this.withTips = withTips;
            this.excluded = excluded;
        }

        // Organisation der Klickmethoden
        public void SetCaller<T>(T caller)
        {
            if (caller is GuessedRow)
            {
                this.callingRow = caller as GuessedRow;
                adaptedClick = OnClick;
                typingHandler = OnKeyTyped;
                backspaceHandler = OnBackspace;
            }
            else if (caller is Piece)
            {
                this.callingPiece = caller as Piece;
                adaptedClick = OnClickClose;
                typingHandler = OnKeyTypedClose;
                backspaceHandler = OnBackspaceClose;
            }
        }

        private void Keyboard_Load(object? sender, EventArgs e) => MakeButtons();

        private void MakeButtons()
        {
            //Dieser Button muss zuerst in die Liste, weil ein Enterdruck auf der Form sonst ein A in das Feld schreibt.
            Button CloseKbd = new()
            {
                Size = new Size(160, 50),
                Location = new Point(
                    InitialPositionX + 55,
                    InitialPositionY + 55 * ((short)30).WrapRow(5, false)
                ),
                BackgroundImage = Image.FromFile("assets/key.png"),
                ForeColor = Color.White,
                UseVisualStyleBackColor = false,
                Text = "Schließen"
            };

            buttons.Add(CloseKbd);
            short j = 1;
            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (withTips && excluded.Contains(c))
                {
                    continue;
                }

                Button button = new()
                {
                    Text = c.ToString(),
                    Size = new Size(50, 50),
                    Location = new Point(
                        InitialPositionX + 55 * j.WrapCol(5, false),
                        InitialPositionY + 55 * j.WrapRow(5, false)
                    ),
                    BackgroundImage = Image.FromFile("assets/key.png"),
                    ForeColor = Color.White,
                };
                buttons.Add(button);

                // j bestimmt nur die Kachelposition für den Buchstaben.
                if (j == 3 || j == 24) j++;
                j++;
            }

            buttons.ForEach(x => { x.Click += adaptedClick; Controls.Add(x); });
            CloseKbd.Click -= adaptedClick;
            CloseKbd.Click += Close;
        }

        #region clickhandler
        //Click auf "Tastaturbutton" - mehrere Buchstaben anfügen, dann erst schließen
        private void OnClick(object? sender, EventArgs e) => callingRow?.SetLetter((sender as Button)?.Text[0]);
        private void OnKeyTyped(Button? typed) => callingRow?.SetLetter(typed?.Text[0]);
        private void OnBackspace() => callingRow?.RemoveLetter();
        private void Close(Object? sender, EventArgs e) => Close();

        //Wenn auf ein Buchstabenfeld geklickt wird, nur dort einen Buchstaben anfügen
        private void OnClickClose(object? sender, EventArgs e)
        {
            callingPiece?.SetLetter((sender as Button)?.Text[0]);
            Close();
        }
        private void OnKeyTypedClose(Button? typed)
        {
            callingPiece?.SetLetter(typed?.Text[0]);
            Close();
        }

        private void OnBackspaceClose()
        {
            callingPiece?.RemoveLetter();
            Close();
        }
        #endregion

        #region keyevents
        private void Keyboard_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                Dispose();
            }
            else if (e.KeyValue == (int)Keys.Escape)
            {
                e.Handled = true;
                Dispose();
            }
            else if (e.KeyValue == (int)Keys.Back)
            {
                e.Handled = true;
                backspaceHandler?.Invoke();
            }
        }

        private void Keyboard_KeyDown(object sender, KeyEventArgs e)
        {
            char typedKey = (char)e.KeyValue;
            if (typedKey >= 'A' && typedKey <= 'Z')
            {
                e.Handled = true;

                current_representative = buttons?.FirstOrDefault(x => x.Text == "" + typedKey);
                current_representative?.Hide();
                //Refresh();
                Thread.Sleep(10);
                typingHandler?.Invoke(current_representative);
                current_representative?.Show();
            }
        }
        #endregion
    }

}
