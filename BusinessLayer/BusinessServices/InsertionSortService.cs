using BusinessLayer.SortingAlgorithms;
using Core.Extensions;

internal class InsertionSortService : ISortingService
{
    public List<int> Sort(List<int> numbers)
    {
        for(var i = 1; i < numbers.Count; i++)
        {
            for(var j = i; j > 0 && numbers[j-1] > numbers[j]; j--)
            {
                numbers.Swap(j-1, j);
            }
        }
        return numbers;
    }
}