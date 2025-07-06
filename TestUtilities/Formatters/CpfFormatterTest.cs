using Utilities.Formatters;

namespace TestUtilities.Formatters;

public class CpfFormatterTest
{

    [Fact]
    public void AddMask_EmptyNull_Returns_EmptyString()
    {
        string emptyString = string.Empty;
        Assert.Equal(emptyString, CpfFormatter.AddMask(null));
        Assert.Equal(emptyString, CpfFormatter.AddMask(emptyString));
    }

    [Fact]
    public void AddMask_ValidsCpfs_Returns_CpfsWithMask_WithDictionary()
    {
        var testCases = new Dictionary<string, string>
        {
            { "45678912345", "456.789.123-45" },
            { "456.789.123-45", "456.789.123-45" },
            { "  45678912344  ", "456.789.123-44" },
            { "456.789.123-33", "456.789.123-33" },
            { "22678912342", "226.789.123-42" },
            { "abc44678912xyz345", "446.789.123-45" },
        };

        foreach (var (input, expected) in testCases)
        {
            string resultado = CpfFormatter.AddMask(input);
            Assert.Equal(expected, resultado);
        }
    }

    [Fact]
    public void AddMask_ValidsCpfsWithMask_Returns_CpfsWithMask_WithList()
    {
        var testCases = new Dictionary<string, string>
        {
            { "456.789.123-45", "456.789.123-45" },
            { "123.456.789-09", "123.456.789-09" },
            { "987.654.321-00", "987.654.321-00" },
            { "000.000.000-00", "000.000.000-00" },
            { "111.222.333-44", "111.222.333-44" },
            { " 555.666.777-88 ", "555.666.777-88" },
            { "\t999.888.777-66\n", "999.888.777-66" },
            { "256.789.123-45", "256.789.123-45" },
        };

        foreach (var (input, expected) in testCases)
        {
            string resultado = CpfFormatter.AddMask(input);
            Assert.Equal(expected, resultado);
        }
    }
}
