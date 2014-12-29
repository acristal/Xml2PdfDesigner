using System;
using System.Text;

namespace Xml2PdfDesigner.Utils
{
    public static class String
    {
        private static readonly Random Random = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        ///     Generate a random string of <see cref="size" /> characters.
        /// </summary>
        /// <param name="size">String length</param>
        /// <returns>A random string</returns>
        public static string RandomString(this int size)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < size; i++)
            {
                builder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Random.NextDouble() + 65))));
            }

            return builder.ToString();
        }
    }
}