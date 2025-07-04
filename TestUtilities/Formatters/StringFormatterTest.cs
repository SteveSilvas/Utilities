using Utilities.Formatters;

namespace TestUtilities;

public class StringFormatterTest
{
    [Fact]
    public void CleanString_EmptyOrNullValue_ReturnsEmptyString()
    {
        var valuesList = new List<string?>
        {
            null,
            "",
        };

        foreach (string? item in valuesList)
        {
            string result = StringFormatter.CleanSpaces(item);

            Assert.Equal(string.Empty, result);
        };
    }

    [Fact]
    public void CleanString_Spaces_ReturnsEmptyString()
    {
        string textWithSpace = "    ";

        string result = StringFormatter.CleanSpaces(textWithSpace);

        Assert.Equal(string.Empty, result);
    }


    [Fact]
    public void CleanString_ValueWithSpaces_ReturnsValueWithoutSpaces()
    {
        string textWithSpace = "   meu texto com espaços     ";

        string result = StringFormatter.CleanSpaces(textWithSpace);

        Assert.Equal("meu texto com espaços", result);
    }
}
