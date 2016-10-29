using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PhotoManager.Utils
{
    public static class NumberUtils
    {
        public static int RandomIntInRange(int rangeStart, int rangeEnd)
        {
            Wait.WaitMiliSeconds(150);
            Random random = new Random();
            return random.Next(rangeStart, rangeEnd);
        }

        public static double GetDoubleFromStr(string stringWithSomeNumberInside)
            {
            double numd = 0.00;
            Regex doublePattern = new Regex("(\\d{1,3}\\,)*(\\d{1,3}){1}(\\.\\d+)");
            Match match = doublePattern.Match(stringWithSomeNumberInside);
            if (match.Success)
            {
                string doubleStr = match.Groups[0].ToString();
                //Logger.Info(typeof(NumberUtils), string.Format("Double: {0} extracted from string: {1}", doubleStr, stringWithSomeNumberInside));
                numd = Convert.ToDouble(match.Groups[0].Value, CultureInfo.InvariantCulture);
            }
            return numd;
        }
    }
}
