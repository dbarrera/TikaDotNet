using System;

namespace TikaDotNet.TextExtraction
{
    public static class StringExtensions
    {
        public static string ToFormat(this string formatMe, params object[] args)
        {
            return String.Format(formatMe, args);
        }
    }
}
