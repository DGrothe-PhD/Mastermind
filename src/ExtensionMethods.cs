namespace MastermindVariante
{
    internal static class ExtensionMethods
    {
        /// <summary>
        /// Custom wrapping of any objects by their number (e.g. index of array element).<br/>
        /// Example usage: Calendar, a tiled layouts <br/>for UI element positions,
        /// such as squares of a chess board, or buttons for customized keyboards.
        /// </summary>
        /// <param name="value">The number of an object. Such as index of an array element</param>
        /// <param name="length">Item number at which a new row should begin</param>
        /// <param name="wrapOnce">Set false for rectangular grids, true for only two rows, ex. 5 -> 2 + 3 elements</param>
        /// <returns>Column position</returns>
        internal static short WrapCol(this short value, short length, bool wrapOnce = true)
        {
            if (wrapOnce)
            {
                return (value < length) ? value: (short)(value - length);
            }
            
            return (short)(value % length);
        }

        /// <summary>
        /// Custom wrapping of any objects by their number (e.g. index of array element).<br/>
        /// Example usage: Calendar, a tiled layouts <br/>for UI element positions,
        /// such as squares of a chess board, or buttons for customized keyboards.
        /// </summary>
        /// <param name="value">The number of an object. Such as index of an array element</param>
        /// <param name="length">Item number at which a new row should begin</param>
        /// <param name="wrapOnce">Set false for rectangular grids, true for only two rows, ex. 5 -> 2 + 3 elements</param>
        /// <returns>Row position</returns>
        internal static short WrapRow(this short value, short length, bool wrapOnce = true)
        {
            if (wrapOnce)
            {
                return value < length ? (short)0 : (short)1;
            }

            return (short)(value / length);
        }

        // Randomly pick a string from an array.
        internal static string Pick(this string[] words)
        {
            Random random = new();
            return words[random.Next(words.Length + 1)];
        }

        internal static Point MoveBy(this Point p, int x, int y)
        {
            return new Point(p.X + x, p.Y + y);
        }
    }

}
