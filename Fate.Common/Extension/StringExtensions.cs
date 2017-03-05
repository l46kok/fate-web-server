using System;

namespace Fate.Common.Extension
{
    public static class StringExtensions
    {
        public static bool EqualsIgnoreCase(this string str, string strToCompare, 
                                            StringComparison comparisonMethod = StringComparison.InvariantCultureIgnoreCase)
        {
            return string.Equals(str, strToCompare, comparisonMethod);
        }
    }
}
