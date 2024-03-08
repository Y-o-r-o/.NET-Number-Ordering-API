using BusinessLayer.Enums;
using BusinessLayer.SortingAlgorithms;

namespace BusinessLayer;

internal static class Constants
{
    public static readonly Dictionary<SortingAlgorithm, ISortingService> SortingAlgorithms = new()
    {
        {SortingAlgorithm.BubbleSort, new BubbleSortService()},
        {SortingAlgorithm.QuickSort, new QuickSortService()},
        {SortingAlgorithm.InsertionSort, new InsertionSortService()}
    };
}

