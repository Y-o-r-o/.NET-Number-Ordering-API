using Core.Enums;
using Core.Exceptions;
using Core.Extensions;
using FluentAssertions;

namespace UnitTests.BusinessLayerTests;

[TestFixture]
public class ValidatorsTests
{
    [Test]
    [TestCase("file.txt")]
    [TestCase("file.tx t")]
    [TestCase("file.t.x t-t")]
    [TestCase("1.1")]
    [TestCase(".gitignore")]
    [TestCase("Name")]
    [TestCase("Name-")]
    [TestCase("MaS09,a .s_d-fa")]
    public void Validate_FileName_DoesntThrow(string fileName)
    {
        // Arrange

        // Act
        var action = () => fileName.Validate(StringIs.FileName);

        // Assert
        action.Should().NotThrow();
    }

    [Test]
    [TestCase("\\/,:*?\"<>|")]
    [TestCase("<")]
    [TestCase("/file.txt")]
    [TestCase("\\file.txt")]
    [TestCase("file\\exe")]
    public void Validate_FileNameWithInvalidSymbols_ThrowSymbolsInvalid(string fileName)
    {
        // Arrange

        // Act
        var action = () => fileName.Validate(StringIs.FileName, "fileName");

        // Assert
        var ex = action.Should().Throw<ValidationException>().Which;
        var haveThisMessage = ex.VariableErrors.Value.Any(str => str == $"`fileName` can't contain any of the following characters `\\`, `/`, `:`, `*`, `?`, `\"`, `<`, `>`, `|`.");
        haveThisMessage.Should().BeTrue();
    }

    [Test]
    [TestCase("")]
    [TestCase("file.txxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxt")]
    public void Validator_FileNameWithInvalidLength_ThrowLengthInvalid(string fileName)
    {
        // Arrange

        // Act
        var action = () => fileName.Validate(StringIs.FileName, "fileName");

        // Assert
        var ex = action.Should().Throw<ValidationException>().Which;
        var haveThisMessage = ex.VariableErrors.Value.Any(str => str == $"`fileName` length should be between 1 and 256.");
        haveThisMessage.Should().BeTrue();
    }

    [Test]
    [TestCase("D:\\TEST\\")]
    [TestCase("C:\\folder1\\folder2\\folder3")]
    [TestCase("C:\\folder with spaces")]
    [TestCase("D:\\folder_with\\sym.bols")]
    [TestCase("D:\\")]
    public void Validate_FilePath_DoesntThrow(string filePath)
    {
        // Arrange

        // Act
        var action = () => filePath.Validate(StringIs.Path);

        // Assert
        action.Should().NotThrow();
    }

    [Test]
    [TestCase("D:\\TEST?")]
    [TestCase("C:\\\\")]
    [TestCase("C\\")]
    [TestCase("<:\\Test")]
    [TestCase("C:\\Test\\Test\\<")]
    [TestCase("C://Test//")]
    public void Validate_FilePathWithInvalidSymbols_ThrowSymbolsInvalid(string filePath)
    {
        // Arrange

        // Act
        var action = () => filePath.Validate(StringIs.Path, "filePath");

        // Assert
        var ex = action.Should().Throw<ValidationException>().Which;
        var haveThisMessage = ex.VariableErrors.Value.Any(str => str == $"`filePath` is not a valid directory path.");
        haveThisMessage.Should().BeTrue();
    }

    [Test]
    [TestCase("1")]
    [TestCase("1.1")]
    [TestCase("1.1 1.1 1.1")]
    [TestCase("1.2345678901234568")]
    [TestCase("-1")]
    [TestCase("1 2")]
    [TestCase("1 1 1")]
    [TestCase("1 2 3 4 5 6 7 8 9")]
    [TestCase("1 9 99 100 123456789")]
    [TestCase("1 -9 9.9 -100 -123.456789")]
    public void Validate_NumbersLine_DoesntThrow(string numbers)
    {
        // Arrange

        // Act
        var action = () => numbers.Validate(StringIs.NumbersLine);

        // Assert
        action.Should().NotThrow();
    }

    [Test]
    [TestCase(" ")]
    [TestCase(" 1")]
    [TestCase("1 ")]
    [TestCase("1,1")]
    [TestCase("a")]
    [TestCase("1 a 2")]
    [TestCase("@")]
    [TestCase("100O00")]
    [TestCase("01 002 003")]
    [TestCase("-01 -9 99")]
    [TestCase("1 -09 -99")]
    public void Validate_NumbersLineWithInvalidSymbols_ThrowSymbolsInvalid(string numbers)
    {
        // Arrange

        // Act
        var action = () => numbers.Validate(StringIs.NumbersLine, "numbers");

        // Assert
        var ex = action.Should().Throw<ValidationException>().Which;
        var haveThisMessage = ex.VariableErrors.Value.Any(str => str == $"`numbers` should only consist of numbers separated by white space.");
        haveThisMessage.Should().BeTrue();
    }
}