using Lang;

namespace MastermindVariante
{
    public delegate void typingHandler(Button? typed);
    public delegate void BackspaceHandler();
    public partial class Keyboard : FormDarkMode
    {
        private readonly int InitialPositionX, InitialPositionY;
        private readonly List<Button> buttons;
        private readonly List<char> excluded;
        private readonly Image keyBackground;

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
            CurrentConfiguration.ApplyLanguage();
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            if (GuessedRow.Caller != null)
                Location = GuessedRow.Caller.Location.MoveBy(10, (int)GuessedRow.Caller.Height / 5);

            //Size = Size.Rescale(Parameter.resizePercentage);

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

            keyBackground = Properties.Resources.key;

            KeyPreview = true;
            buttons = new();
            excluded = new List<char>();
        }

        public Keyboard(bool withTips, List<char> excluded) : this()
        {
            this.withTips = withTips;
            this.excluded = excluded;
        }

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
            //This button must be placed first as first button captures return key.
            Button CloseKbd = new()
            {
                Size = new Size(160, 50),
                Location = new Point(
                    InitialPositionX + 55 * Parameter.resizePercentage / 100,
                    InitialPositionY + 55 * Parameter.resizePercentage / 100 * ((short)30).WrapRow(5, false)
                ),
                BackgroundImage = keyBackground,
                ForeColor = Color.White,
                UseVisualStyleBackColor = false,
                Text = Resources.CloseKeyboard
            };

            CloseKbd.Font = CloseKbd.Font.Resize(Parameter.resizePercentage);
            CloseKbd.Size = CloseKbd.Size.Rescale(Parameter.resizePercentage);

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
                        InitialPositionX + 55 * Parameter.resizePercentage / 100 * j.WrapCol(5, false),
                        InitialPositionY + 55 * Parameter.resizePercentage / 100 * j.WrapRow(5, false)
                    ),
                    BackgroundImage = keyBackground,
                    ForeColor = Color.White,
                };
                button.Font = button.Font.Resize(Parameter.resizePercentage);
                button.Size = button.Size.Rescale(Parameter.resizePercentage);

                buttons.Add(button);

                // j is for tile position of letter keys.
                if (j == 3 || j == 24) j++;
                j++;
            }

            buttons.ForEach(x => { x.Click += adaptedClick; Controls.Add(x); });
            CloseKbd.Click -= adaptedClick;
            CloseKbd.Click += Close;
        }

        #region clickhandler
        //click on or type several keys for that row
        private void OnClick(object? sender, EventArgs e) => callingRow?.SetLetter((sender as Button)?.Text[0]);
        private void OnKeyTyped(Button? typed) => callingRow?.SetLetter(typed?.Text[0]);
        private void OnBackspace() => callingRow?.RemoveLetter();
        private void Close(Object? sender, EventArgs e) => Close();

        //enter a single letter for a field
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
