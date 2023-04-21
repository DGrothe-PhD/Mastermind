using MastermindVariante.Properties;
using System.Runtime.InteropServices;

namespace MastermindVariante
{
    // Dieser Delegat lauscht, ob in einem Control an bestimmter Stelle "ein bestimmter Inhalt ist".
    // Inhalt kann Verschiedenes sein: Beim Label ist es das Text-Property. Beim Panel kann es das gewählte Bild sein.
    // Der Aufrufer des Delegaten wird selbst einen Text ausliefern, der dargestellt werden soll.
    public delegate void UIFieldChanged<T, U>(T control, U content) where T : Control;

    // Parameter der grafischen Elemente für Buchstaben und Auswertung.
    public abstract class Parameter
    {
        public const int initialPositionY = 48;
        public const int initialPositionX = 17;
        public const int height = 58;
        public const int paddingBottom = 4;
        public const int paddingLeft = 4;
        public const int width = 58;
        public const short pinsPerRow = 2;

        public static void SetWoodenAppearance<T, U>(T elm, U? content) where T : Control
        {
            elm.BackColor = Color.Transparent;
            if (String.IsNullOrEmpty(content?.ToString()))
            {
                elm.BackgroundImage = null;
                //elm.BackgroundImage = Image.FromFile("nothing.png");
            }
            else
            {
                char c = (content?.ToString() ?? " ")[0];
                int flip = (elm.Location.Y + elm.Location.X) % 8;
                var img = Image.FromFile($"assets/wood{(1 + (int)c % 19):00}.png");
                img.RotateFlip((RotateFlipType)flip);
                elm.BackgroundImage = img;
                elm.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

    }
}