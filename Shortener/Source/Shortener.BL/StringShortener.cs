using System;
using System.Linq;

namespace TestTask.Shortener.BL
{
    public static class StringShortener
    {
        public static readonly string Alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
        public static readonly Int32 Base = Alphabet.Length;

        public static string Encode(Int32 i)
        {
            if (i == 0) return Alphabet[0].ToString();

            var s = string.Empty;

            while (i > 0)
            {
                s += Alphabet[i % Base];
                i = i / Base;
            }

            return string.Join(string.Empty, s.Reverse());
        }

        public static Int32 Decode(string s)
        {
            var i = 0;

            foreach (var c in s)
            {
                i = (i * Base) + Alphabet.IndexOf(c);
            }

            return i;
        }
    }
}

