namespace Core;

public interface IFileIOManager
{
    public Task WriteStringAsync(string textToSave);

    public Task<string> ReadStringAsync();
}