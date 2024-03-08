using BusinessLayer.SortingAlgorithms;
using Core.Extensions;

internal class BubbleSortService() : ISortingService
{
    public List<double> Sort(List<double> numbers)
    {
        bool swapped;

        do
        {
            swapped = false;

            for (var idx = 0; idx < numbers.Count - 1; idx++)
            {
                if (numbers[idx] > numbers[idx + 1])
                {
                    numbers.Swap(idx, idx + 1);
                    swapped = true;
                }
            }
        } while (swapped);

        return numbers;
    }
}