namespace Core.Extensions;

public static class IntExtensions
{
    public static string ConvertToString(this IEnumerable<int> integers)
    {
        return string.Join(" ", integers);
    }
}
