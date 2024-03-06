using BusinessLayer.Interfaces;

namespace BusinessLayer.BusinessServices;

internal class NumberService : INumberService
{
    public Task<string> CreateNumbersAsync(string numbers)
    {
        throw new NotFiniteNumberException();
    }

    public Task<string> GetNumbersAsync()
    {
        throw new NotFiniteNumberException();
    }
}