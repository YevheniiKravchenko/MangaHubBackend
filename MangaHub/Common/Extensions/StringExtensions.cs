using SmartFormat;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string Format(this string str, params object[] @params)
        {
            return Smart.Format(str, @params);
        }
    }

}
