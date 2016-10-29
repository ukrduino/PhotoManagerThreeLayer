using System;
using System.Linq;

namespace PhotoManager.Utils
{
     public static class StringUtils
    {
        public static string RandomAlphaNumericalStr(int length)
        {
            Wait.WaitMiliSeconds(150);
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomAlphabeticalStr(int length)
        {
            Wait.WaitMiliSeconds(150);
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomNumStr(int length)
        {
            Wait.WaitMiliSeconds(150);
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomGender()
        {
            Wait.WaitMiliSeconds(150);
            Random random = new Random(Environment.TickCount);
            const string chars = "MF";
            return new string(Enumerable.Repeat(chars, 1).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomNumStrInRange(int rangeStart, int rangeEnd)
        {
            return NumberUtils.RandomIntInRange(rangeStart, rangeEnd).ToString();
        }

        public static bool RandomBool()
        {
            Wait.WaitMiliSeconds(150);
            Random random = new Random();
            return random.NextDouble() >= 0.5;
        }
    }
}
