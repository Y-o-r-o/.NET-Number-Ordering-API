namespace BusinessLayer.Interfaces;

public interface INumberService
{
    public Task<string> CreateNumbersAsync(string numbers);

    public Task<string> GetNumbersAsync();
}