using Core.Extensions;
using FluentAssertions;

namespace UnitTests.BusinessLayerTests;

[TestFixture]
public class SortingServicesTests
{
    [Test]
    [TestCase("1", "1")]
    [TestCase("1 2 3", "1 2 3")]
    [TestCase("1 3 2", "1 2 3")]
    [TestCase("2 3 2", "2 2 3")]
    [TestCase("2 2 2", "2 2 2")]
    [TestCase("-1 1 0", "-1 0 1")]
    [TestCase("1.1 1.3 -1.2", "-1.2 1.1 1.3")]
    [TestCase("1.234567890123456", "1.234567890123456")]
    [TestCase("1.234567890123456 1.234567890123457 1.234567890123455", "1.234567890123455 1.234567890123456 1.234567890123457")]
    public void SortAsync_GiveNumbers_ReturnsSortedNumbers(string numbers, string expectedResult)
    {
        // Arrange
        var bubbleSortService = new BubbleSortService();
        var quickSortService = new QuickSortService();
        var insertionSortService = new InsertionSortService();

        // Act
        var result1 = bubbleSortService.Sort(numbers.ConvertToDoubleList()).ConvertToString();
        var result2 = quickSortService.Sort(numbers.ConvertToDoubleList()).ConvertToString();
        var result3 = insertionSortService.Sort(numbers.ConvertToDoubleList()).ConvertToString();

        // Assert
        result1.Should().BeEquivalentTo(expectedResult);
        result2.Should().BeEquivalentTo(expectedResult);
        result3.Should().BeEquivalentTo(expectedResult);
    }
}