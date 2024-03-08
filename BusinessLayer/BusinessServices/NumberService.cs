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

        var doubleNumbers = numbers.ConvertToDoubleList();

        var sortedNumbers = sortingService.Sort(doubleNumbers).ConvertToString();

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
            var task = GetNumbersSortingAlgorithmTimeAsync(numbers, sortingAlgorithm);

            tasks.Add(task);
        }

        var results = await Task.WhenAll(tasks);
        return results.ToList();
    }

    public async Task<string> GetNumbersSortingAlgorithmTimeAsync(string numbers, SortingAlgorithm sortingAlgorithm)
    {
        var sortingService = GetSortingService(sortingAlgorithm);
        var doubleNumbers = numbers.ConvertToDoubleList();

        var stopwatch = Stopwatch.StartNew();

        sortingService.Sort(doubleNumbers);

        stopwatch.Stop();
        return await Task.FromResult($"{sortingAlgorithm} algorithm took {stopwatch.ElapsedTicks} ticks.");
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