using MastermindVariante.Properties;

namespace MastermindVariante
{
    // This delegate looks if there is "a certain content in a control at a certain position".
    // Content can be various things: For the label it is the text property. For a panel it can be the selected image.
    // The caller of the delegate will deliver a text to be displayed.
    public delegate void UIFieldChanged<T, U>(T control, U content) where T : Control;

    // Parameter der grafischen Elemente für Buchstaben und Auswertung.
    public abstract class Parameter
    {
        public const int DefaultElementHeight = 30;
        public const int DefaultTopMargin = 15;

        public static readonly int screenheight = Screen.PrimaryScreen.Bounds.Height;
        public static readonly int resizePercentage = (screenheight > 1000) ? 100 : 75;

        public static readonly int initialPositionY = Math.Max(48 * resizePercentage / 100, DefaultElementHeight + DefaultTopMargin);
        public static readonly int initialPositionX = 17 * resizePercentage / 100;
        public static readonly int height = 58 * resizePercentage / 100;
        public static readonly int paddingBottom = 4 * resizePercentage / 100;
        public static readonly int paddingLeft = 4 * resizePercentage / 100;
        public static readonly int width = 58 * resizePercentage / 100;
        public const short pinsPerRow = 2;


        public static readonly List<Image> WoodTiles = new()
        {
            Resources.wood01, Resources.wood02, Resources.wood03, Resources.wood04, Resources.wood05,
            Resources.wood06, Resources.wood07, Resources.wood08, Resources.wood09, Resources.wood10,
            Resources.wood11, Resources.wood12, Resources.wood13, Resources.wood14, Resources.wood15,
            Resources.wood16, Resources.wood17, Resources.wood18, Resources.wood19
        };

        private static Image? WoodenTile(char c)
        {
            try
            {
                int i = (int)c % 19;
                return WoodTiles[i];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void SetWoodenAppearance<T, U>(T elm, U? content) where T : Control
        {
            elm.BackColor = Color.Transparent;
            if (String.IsNullOrEmpty(content?.ToString()))
            {
                elm.BackgroundImage = null;
            }
            else
            {
                char c = (content?.ToString() ?? " ")[0];
                int flip = (elm.Location.Y + elm.Location.X) % 8;

                Image? img = WoodenTile(c);
                if (img == null) { return; }

                img.RotateFlip((RotateFlipType)flip);
                elm.BackgroundImage = img;
                elm.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

    }
}