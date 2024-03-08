using BusinessLayer.SortingAlgorithms;
using Core.Extensions;

internal class QuickSortService : ISortingService
{
    public List<double> Sort(List<double> numbers)
    {
        QuickSort(numbers, 0, numbers.Count - 1);
        return numbers;
    }

    private static void QuickSort(List<double> numbers, int low, int high)
    {
        if (low < high)
        {
            var partitionIndex = Partition(numbers, low, high);
            QuickSort(numbers, low, partitionIndex - 1);
            QuickSort(numbers, partitionIndex + 1, high);
        }
    }

    private static int Partition(List<double> numbers, int low, int high)
    {
        var pivot = numbers[high];
        var i = low;

        for (var j = low; j < high; j++)
        {
            if (numbers[j] <= pivot)
            {
                numbers.Swap(i,j);
                i++;
            }
        }

        numbers.Swap(i, high);

        return i;
    }

}