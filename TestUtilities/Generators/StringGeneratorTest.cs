using Utilities.Generators;

namespace TestUtilities.Generators;
public class StringGeneratorTest
{
    #region GenerateRandomLetters
    [Fact]
    public void GenerateRandomLetters_WithoutLimit_ReturnsOneRandomChar()
    {
        var result = StringGenerator.GenerateRandomLetters();

        Assert.NotNull(result);
        Assert.Equal(1, result.Length);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(50)]
    public void GenerateRandomLetters_WithLimit_ReturnsCorrectLength(int limit)
    {
        var result = StringGenerator.GenerateRandomLetters(limit);

        Assert.NotNull(result);
        Assert.Equal(limit, result.Length);
    }

    [Fact]
    public void GenerateRandomLetters_ReturnsOnlyLetters()
    {
        const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        var result = StringGenerator.GenerateRandomLetters(100);

        Assert.All(result, c => Assert.Contains(c, validChars));
    }

    [Fact]
    public void GenerateRandomLetters_MultipleCalls_ShouldReturnDifferentValues()
    {
        var result1 = StringGenerator.GenerateRandomLetters(10);
        var result2 = StringGenerator.GenerateRandomLetters(10);

        Assert.NotEqual(result1, result2);
    }
    #endregion

    #region GenerateRandomLetters
    [Fact]
    public void GenerateRandomAphaNumber_WithoutLimit_ReturnsOneRandomChar()
    {
        var result = StringGenerator.GenerateRandomAphaNumber();

        Assert.NotNull(result);
        Assert.Equal(1, result.Length);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(50)]
    public void GenerateRandomAphaNumber_WithLimit_ReturnsCorrectLength(int limit)
    {
        var result = StringGenerator.GenerateRandomAphaNumber(limit);

        Assert.NotNull(result);
        Assert.Equal(limit, result.Length);
    }

    [Fact]
    public void GenerateRandomAphaNumber_ReturnsOnlyLetters()
    {
        const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        var result = StringGenerator.GenerateRandomAphaNumber(100);

        Assert.All(result, c => Assert.Contains(c, validChars));
    }

    [Fact]
    public void GenerateRandomAphaNumber_MultipleCalls_ShouldReturnDifferentValues()
    {
        var result1 = StringGenerator.GenerateRandomAphaNumber(10);
        var result2 = StringGenerator.GenerateRandomAphaNumber(110);

        Assert.NotEqual(result1, result2);
    }
    #endregion
}
