using Utilities.Formatters;

namespace TestUtilities.Formatters;

public class CpfFormatterTest
{
    #region AddMask
    [Fact]
    public void AddMask_EmptyNull_Returns_EmptyString()
    {
        string emptyString = string.Empty;
        Assert.Equal(emptyString, CpfFormatter.AddMask(null));
        Assert.Equal(emptyString, CpfFormatter.AddMask(emptyString));
    }

    [Fact]
    public void AddMask_ValidCpfs_Returns_CpfsWithMask()
    {
        var testCases = new Dictionary<string, string>
        {
            { "45678912345", "456.789.123-45" },
            { "456.789.123-45", "456.789.123-45" },
            { "  45678912344  ", "456.789.123-44" },
            { "abc44678912xyz345", "446.789.123-45" },
        };

        foreach (var (input, expected) in testCases)
        {
            string resultado = CpfFormatter.AddMask(input);
            Assert.Equal(expected, resultado);
        }
    }

    [Fact]
    public void AddMask_InvalidCpfs_Returns_EmptyString()
    {
        var invalidInputs = new[]
        {
            "123",             // menos de 11 dígitos
            "1234567890123",   // mais de 11 dígitos
            "abc",             // só letras
            "",                // vazio
            null
        };

        foreach (var input in invalidInputs)
        {
            string resultado = CpfFormatter.AddMask(input);
            Assert.Equal(string.Empty, resultado);
        }
    }
    #endregion

    #region RemoveMask
    [Fact]
    public void RemoveMask_EmptyNull_Returns_EmptyString()
    {
        string emptyString = string.Empty;
        Assert.Equal(emptyString, CpfFormatter.RemoveMask(null));
        Assert.Equal(emptyString, CpfFormatter.RemoveMask(emptyString));
        Assert.Equal(emptyString, CpfFormatter.RemoveMask("   "));
        Assert.Equal(emptyString, CpfFormatter.RemoveMask("\t\n"));
    }

    [Fact]
    public void RemoveMask_FormattedCpfs_Returns_OnlyNumbers()
    {
        var testCases = new Dictionary<string, string>
        {
            { "123.456.789-11", "12345678911" },
            { "123-456.456-78", "12345645678" },
            { "987.000.785-20", "98700078520" },
            { "000.000.000-00", "00000000000" },
            { " 12345678910 ", "12345678910" },
        };

        foreach (var (input, expected) in testCases)
        {
            string resultado = CpfFormatter.RemoveMask(input);
            Assert.Equal(expected, resultado);
        }
    }

    [Fact]
    public void RemoveMask_OnlyNumbers_Returns_SameNumbers()
    {
        var testCases = new Dictionary<string, string>
        {
            { "12345678911", "12345678911" },
            { "98765432100", "98765432100" },
            { "00000000000", "00000000000" },
            { "11111111111", "11111111111" },
            { "99999999999", "99999999999" },
        };

        foreach (var (input, expected) in testCases)
        {
            string resultado = CpfFormatter.RemoveMask(input);
            Assert.Equal(expected, resultado);
        }
    }

    [Fact]
    public void RemoveMask_InvalidStrings_Returns_EmptyString()
    {
        var testCases = new[]
        {
            "abcdefghijklmnop",
            "!@#$%^&*()_+",
            "..//--==",
            "   ",
            "\t\n\r",
        };

        foreach (var input in testCases)
        {
            string resultado = CpfFormatter.RemoveMask(input);
            Assert.Equal(string.Empty, resultado);
        }
    }

    [Fact]
    public void RemoveMask_RoundTrip_Returns_OriginalCpf()
    {
        var originalCpfs = new[]
        {
            "12345678911",
            "98765432100",
            "00000000000",
            "11122233344"
        };

        foreach (var original in originalCpfs)
        {
            string withMask = CpfFormatter.AddMask(original);
            string removedMask = CpfFormatter.RemoveMask(withMask);
            Assert.Equal(original, removedMask);
        }
    }
    #endregion
}
