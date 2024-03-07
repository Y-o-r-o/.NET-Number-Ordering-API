using BusinessLayer.Interfaces;
using Core;

namespace BusinessLayer.BusinessServices;

internal class NumberService(IFileIOManager fileIOManager) : INumberService
{
    private readonly IFileIOManager _fileIOManager = fileIOManager;

    public Task<string> CreateNumbersAsync(string numbers)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetNumbersAsync()
    {
        throw new NotImplementedException();
    }
}