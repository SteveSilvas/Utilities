using Utilities.Generators;

namespace TestUtilities.Generators;

public class AgeGeneratorTest
{
    private static readonly DateTime FixedToday = new DateTime(2025, 07, 09);
    [Theory]
    [InlineData("2000-06-10", 25)] // aniversário já passou
    [InlineData("2000-12-25", 24)] // aniversário ainda vai chegar
    [InlineData("2025-07-09", 0)]  // nascido hoje
    public void Get_DateOfBirth_ReturnsCorrectAge(string dateOfBirthStr, int expectedAge)
    {
        DateTime dateOfBirth = DateTime.Parse(dateOfBirthStr);
        int result = AgeGenerator.Get(dateOfBirth, FixedToday);
        Assert.Equal(expectedAge, result);
    }

    [Fact]
    public void Get_EmptyDateTime_ReturnsZero()
    {
        DateTime emptyDateTime = new DateTime(); // 01/01/0001 00:00:00
        int result = AgeGenerator.Get(emptyDateTime, FixedToday);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Get_MinDateTime_ReturnsZero()
    {
        DateTime minDateTime = DateTime.MinValue; // 01/01/0001 00:00:00
        int result = AgeGenerator.Get(minDateTime, FixedToday);
        Assert.Equal(0, result);
    }
}