using System;

namespace Cp68Minion.Utils
{
    public class StringUtils
    {
        public static string RemoveBraces(string value)
        {
            return value.Substring(0, value.IndexOf("\n", StringComparison.Ordinal));
        }
    }
}