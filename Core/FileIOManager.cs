using System.Net;
using Core.Enums;
using Core.Exceptions;
using Core.Extensions;

namespace Core;

public class FileIOManager : IFileIOManager
{
    private readonly string _file;

    public FileIOManager(string filePath, string fileName)
    {
        filePath.Validate(StringIs.Path, "filePath");
        fileName.Validate(StringIs.FileName, "fileName");

        _file = Path.Combine(filePath, fileName);
    }

    public async Task WriteStringAsync(string textToSave)
    {
        try
        {
            using var outputFile = new StreamWriter(_file);

            await outputFile.WriteAsync(textToSave);
        }
        catch (IOException)
        {
            throw new HttpResponseException(HttpStatusCode.InternalServerError, $"The file could not be written. Try to check if path exist: {_file}");
        }
    }

    public async Task<string> ReadStringAsync()
    {
        try
        {
            using var streamReader = new StreamReader(_file);

            return await streamReader.ReadToEndAsync();
        }
        catch (IOException)
        {
            throw new HttpResponseException(HttpStatusCode.InternalServerError, $"The file could not be read. Try to check if file or path exist: {_file}");
        }
    }

}