namespace EyapLibrary.Extensions
{
    using System;
    using System.IO;

    public static class StringExtensions
    {
        /// <summary>
        /// Returns <paramref name="value"/> with the minimal concatenation of <paramref name="starting"/> that
        /// results in satisfying .StartsWith(starting).
        /// </summary>
        /// <example>"llo".WithStarting("hel") returns "hello", which is the result of "he" + "llo".</example>
        public static string WithStarting(this string value, string starting)
        {
            if (value == null)
            {
                return starting;
            }

            // Left() is 1-indexed, so include these cases
            // * Append no characters
            // * Append up to N characters, where N is ending length
            for (int i = 0; i < starting.Length; i++)
            {
                string tmp = starting.Left(i) + value;
                if (tmp.StartsWith(starting))
                {
                    return tmp;
                }
            }
            return starting + value;
        }

        /// <summary>
        /// Returns <paramref name="value"/> with the minimal concatenation of <paramref name="ending"/> (starting from end) that
        /// results in satisfying .EndsWith(ending).
        /// </summary>
        /// <example>"hel".WithEnding("llo") returns "hello", which is the result of "hel" + "lo".</example>
        public static string WithEnding(this string value, string ending)
        {
            if (value == null)
            {
                return ending;
            }

            // Right() is 1-indexed, so include these cases
            // * Append no characters
            // * Append up to N characters, where N is ending length
            for (int i = 0; i < ending.Length; i++)
            {
                string tmp = value + ending.Right(i);
                if (tmp.EndsWith(ending))
                {
                    return tmp;
                }
            }
            return value + ending;
        }

        /// <summary>Gets the leftmost <paramref name="length" /> characters from a string.</summary>
        /// <param name="value">The string to retrieve the substring from.</param>
        /// <param name="length">The number of characters to retrieve.</param>
        /// <returns>The substring.</returns>
        public static string Left(this string value, int length)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", length, "Length is less than zero.");
            }

            return (length < value.Length) ? value.Substring(0, length) : value;
        }

        /// <summary>Gets the rightmost <paramref name="length" /> characters from a string.</summary>
        /// <param name="value">The string to retrieve the substring from.</param>
        /// <param name="length">The number of characters to retrieve.</param>
        /// <returns>The substring.</returns>
        public static string Right(this string value, int length)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", length, "Length is less than zero.");
            }

            return (length < value.Length) ? value.Substring(value.Length - length) : value;
        }
    }
}
