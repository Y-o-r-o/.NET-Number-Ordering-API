using System.Diagnostics;
using System.Net;
using BusinessLayer.Enums;
using BusinessLayer.Interfaces;
using BusinessLayer.SortingAlgorithms;
using Core;
using Core.Enums;
using Core.Exceptions;
using Core.Extensions;

namespace BusinessLayer.BusinessServices;

internal class NumberService(IFileIOManager fileIOManager) : INumberService
{
    private readonly IFileIOManager _fileIOManager = fileIOManager;

    public async Task<string> CreateNumbersAsync(string numbers, SortingAlgorithm sortingAlgorithm = SortingAlgorithm.BubbleSort)
    {
        numbers.Validate(StringIs.NumbersLine);

        var sortingService = GetSortingService(sortingAlgorithm);

        var intNumbers = numbers.ConvertToIntList();

        var sortedNumbers = sortingService.Sort(intNumbers).ConvertToString();

        await _fileIOManager.WriteStringAsync(sortedNumbers);

        return sortedNumbers;
    }

    public async Task<string> GetNumbersAsync()
    {
        return await _fileIOManager.ReadStringAsync() ?? throw new HttpResponseException(HttpStatusCode.NotFound, "File containing numbers not found.");
    }

    public async Task<List<string>> GetNumbersSortingAlgorithmsTimesAsync(string numbers)
    {
        numbers.Validate(StringIs.NumbersLine);

        var tasks = new List<Task<string>>();
        foreach (var sortingAlgorithm in (SortingAlgorithm[])Enum.GetValues(typeof(SortingAlgorithm)))
        {
            var task = Task.Run(() =>
            {
                var sortingService = GetSortingService(sortingAlgorithm);
                var intNumbers = numbers.ConvertToIntList();

                var stopwatch = Stopwatch.StartNew();

                sortingService.Sort(intNumbers);

                stopwatch.Stop();
                return $"{sortingAlgorithm} algorithm took {stopwatch.ElapsedTicks} ticks.";
            });

            tasks.Add(task);
        }
        
        var results = await Task.WhenAll(tasks);
        return results.ToList();
    }

    private static ISortingService GetSortingService(SortingAlgorithm sortingAlgorithm)
    {
        if (!Constants.SortingAlgorithms.TryGetValue(sortingAlgorithm, out ISortingService? sortingService))
        {
            throw new HttpResponseException(HttpStatusCode.InternalServerError, $"There is no sorting algorithm parameters for enum: {sortingAlgorithm}.");
        };
        return sortingService;
    }
}