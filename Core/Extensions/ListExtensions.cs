namespace Core.Extensions;

public static class ListExtensions
{
    public static void Swap<T>(this List<T> list, int i, int j)
    {
        (list[j], list[i]) = (list[i], list[j]);
    }
}