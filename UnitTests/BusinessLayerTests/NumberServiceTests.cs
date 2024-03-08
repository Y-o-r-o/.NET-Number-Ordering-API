using BusinessLayer.BusinessServices;
using BusinessLayer.Enums;
using Core;
using Core.Exceptions;
using FluentAssertions;
using Moq;
using UnitTests.BusinessLayerTests.MoqSetups;
using static BusinessLayer.BusinessServices.NumberService;

namespace UnitTests.BusinessLayerTests;

[TestFixture]
public class NumberServicesTests
{
    private MockRepository mockRepository;

    private Mock<IFileIOManager> _mockFileIOManager;

    [SetUp]
    public void SetUp()
    {
        mockRepository = new MockRepository(MockBehavior.Strict);

        _mockFileIOManager = mockRepository.Create<IFileIOManager>();
    }

    private NumberService InitializeNumberServices()
    {
        return new NumberService(
            _mockFileIOManager.Object
        );
    }

    [Test]
    [TestCase("1")]
    [TestCase("1 2 3")]
    [TestCase("1 3 2")]
    [TestCase("1 3 3")]
    [TestCase("1 30000 3")]
    public async Task CreateNumbersAsync_GiveValidNumbers_DoesntThrow(string numbers)
    {
        // Arrange
        _mockFileIOManager
           .Setup_WriteString_DoesntThrow();

        var numberService = InitializeNumberServices();

        // Act
        var action = () => numberService.CreateNumbersAsync(numbers);

        // Assert
        await action.Should().NotThrowAsync();
    }

    [Test]
    [TestCase("1", "1")]
    [TestCase("1 2 3", "1 2 3")]
    [TestCase("1 3 2", "1 2 3")]
    [TestCase("2 3 2", "2 2 3")]
    [TestCase("-1 1 0", "-1 0 1")]
    public async Task CreateNumbersAsync_GiveValidNumbers_ReturnsSortedNumbers(string givenNumbers, string expectedResult)
    {
        // Arrange
        _mockFileIOManager
           .Setup_WriteString_DoesntThrow();

        var numberService = InitializeNumberServices();

        // Act
        var result = await numberService.CreateNumbersAsync(givenNumbers);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Test]
    [TestCase(" 1")]
    [TestCase("")]
    [TestCase("01")]
    [TestCase("-01 1 0")]
    public void CreateNumbersAsync_GiveInvalidNumbers_ThrowsValidationException(string givenNumbers)
    {
        // Arrange
        _mockFileIOManager
           .Setup_WriteString_DoesntThrow();

        var numberService = InitializeNumberServices();

        // Act
        var action = () => numberService.CreateNumbersAsync(givenNumbers);

        // Assert
        var ex = action.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task CreateNumbersAsync_GiveValidNumbers_ThrowsFileCouldNotBeWritten()
    {
        // Arrange
        _mockFileIOManager
           .Setup_WriteString_ThrowsFileCouldNotBeWritten();

        var numberService = InitializeNumberServices();

        // Act
        var action = () => numberService.CreateNumbersAsync("1 2 3");

        // Assert
        await action.Should().ThrowAsync<HttpResponseException>().WithMessage("The file could not be written. Try to check if path exist: ____");
    }

    [Test]
    public async Task CreateNumbersAsync_GiveNonExistingSortingAlgorithm_ThrowsThereIsNoSortingAlgorithm()
    {
        // Arrange
        _mockFileIOManager
           .Setup_WriteString_ThrowsFileCouldNotBeWritten();

        var numberService = InitializeNumberServices();

        // Act
        var action = () => numberService.CreateNumbersAsync("1 2 3", (SortingAlgorithm)999);

        // Assert
        await action.Should().ThrowAsync<HttpResponseException>().WithMessage("There is no sorting algorithm parameters for enum: 999.");
    }

    [Test]
    public async Task GetNumbersAsync_DoesntThrow()
    {
        // Arrange
        _mockFileIOManager
           .Setup_ReadString_ReturnString("1 2 3");

        var numberService = InitializeNumberServices();

        // Act
        var action = () => numberService.GetNumbersAsync();

        // Assert
        await action.Should().NotThrowAsync();
    }

    [Test]
    public async Task GetNumbersAsync_ThrowsFileCouldNotBeRead()
    {
        // Arrange
        _mockFileIOManager
           .Setup_ReadString_ThrowsFileCouldNotBeRead();

        var numberService = InitializeNumberServices();

        // Act
        var action = () => numberService.GetNumbersAsync();

        // Assert
        await action.Should().ThrowAsync<HttpResponseException>().WithMessage("The file could not be read. Try to check if file or path exist: _____");
    }

    [Test]
    public async Task GetNumbersAsync_ThrowsFileNotFound()
    {
        // Arrange
        _mockFileIOManager
           .Setup_ReadString_ReturnNull();

        var numberService = InitializeNumberServices();

        // Act
        var action = () => numberService.GetNumbersAsync();

        // Assert
        await action.Should().ThrowAsync<HttpResponseException>().WithMessage("File containing numbers not found.");
    }

    [Test]
    [TestCase("1")]
    [TestCase("1 2 3")]
    [TestCase("1 3 2")]
    [TestCase("1 3 3")]
    [TestCase("1 30000 3")]
    public async Task GetNumbersSortingAlgorithmsTimesAsync_DoesntThrow(string numbers)
    {
        // Arrange
        var numberService = InitializeNumberServices();

        // Act
        var action = () => numberService.GetNumbersSortingAlgorithmsTimesAsync(numbers);

        // Assert
        await action.Should().NotThrowAsync();
    }

    public void GetNumbersSortingAlgorithmsTimesAsync_GiveInvalidNumbers_ThrowsValidationException(string givenNumbers)
    {
        // Arrange
        var numberService = InitializeNumberServices();

        // Act
        var action = () => numberService.GetNumbersSortingAlgorithmsTimesAsync("01");

        // Assert
        var ex = action.Should().ThrowAsync<ValidationException>();
    }
}