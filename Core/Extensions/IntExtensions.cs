namespace Core.Extensions;

public static class DoubleExtensions
{
    public static string ConvertToString(this IEnumerable<double> doubles)
    {
        return string.Join(" ", doubles);
    }
}
