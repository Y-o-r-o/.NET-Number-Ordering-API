using BusinessLayer.Enums;

namespace BusinessLayer.Interfaces;

public interface INumberService
{
    public Task<string> CreateNumbersAsync(string numbers, SortingAlgorithm sortingAlgorithm);

    public Task<string> GetNumbersAsync();

    public Task<List<string>> GetNumbersSortingAlgorithmsTimesAsync(string numbers);
}