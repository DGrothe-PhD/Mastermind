namespace MastermindVariante
{
    internal static class Erweiterungsmethoden
    {
        //Custom wrapping of UI element positions
        internal static short WrapCol(this short value, short length, bool wrapOnce = true)
        {
            if (wrapOnce)
            {
                if (value < length)
                    return value;
                else
                    return (short)(value - length);
            }
            else
                return (short)(value % length);

        }

        internal static short WrapRow(this short value, short length, bool wrapOnce = true)
        {
            if (wrapOnce)
            {
                if (value < length)
                    return 0;
                else
                    return 1;
            }
            else
                return (short)(value / length);
        }

        // Randomly pick a string from an array.
        internal static string Pick(this string[] words)
        {
            Random random = new();
            return words[random.Next(words.Length + 1)];
        }
    }

}
