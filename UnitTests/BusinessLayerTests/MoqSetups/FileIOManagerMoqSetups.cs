using System.Net;
using Core;
using Core.Exceptions;
using Moq;

namespace UnitTests.BusinessLayerTests.MoqSetups;

public static class FileIOManagerMoqSetups
{
    public static Mock<IFileIOManager> Setup_WriteString_DoesntThrow(this Mock<IFileIOManager> mockRepository)
    {
        mockRepository
           .Setup(m => m.WriteStringAsync(It.IsAny<string>()))
           .Returns(Task.CompletedTask);

        return mockRepository;
    }

    public static Mock<IFileIOManager> Setup_WriteString_ThrowsFileCouldNotBeWritten(this Mock<IFileIOManager> mockRepository)
    {
        mockRepository
           .Setup(m => m.WriteStringAsync(It.IsAny<string>()))
           .ThrowsAsync(new HttpResponseException(HttpStatusCode.InternalServerError, "The file could not be written. Try to check if path exist: ____"));

        return mockRepository;
    }

    public static Mock<IFileIOManager> Setup_ReadString_ReturnString(this Mock<IFileIOManager> mockRepository, string str)
    {
        mockRepository
           .Setup(m => m.ReadStringAsync())
           .ReturnsAsync(str);

        return mockRepository;
    }

    public static Mock<IFileIOManager> Setup_ReadString_ThrowsFileCouldNotBeRead(this Mock<IFileIOManager> mockRepository)
    {
        mockRepository
           .Setup(m => m.ReadStringAsync())
           .ThrowsAsync(new HttpResponseException(HttpStatusCode.InternalServerError, "The file could not be read. Try to check if file or path exist: _____"));

        return mockRepository;
    }

    public static Mock<IFileIOManager> Setup_ReadString_ReturnNull(this Mock<IFileIOManager> mockRepository)
    {
        mockRepository
           .Setup(m => m.ReadStringAsync())
           .ReturnsAsync((string)null);

        return mockRepository;
    }
}