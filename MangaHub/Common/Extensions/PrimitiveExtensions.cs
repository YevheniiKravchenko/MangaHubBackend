namespace Common.Extensions
{
    public static class PrimitivesExtensions
    {
        public static bool In<T>(this T primitive, params T[] @params)
        {
            return @params.Contains(primitive);
        }
    }

}
