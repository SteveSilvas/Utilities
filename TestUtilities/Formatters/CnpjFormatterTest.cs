using Utilities.Formatters;

namespace TestUtilities.Formatters;

public class CnpjFormatterTest
{
    #region AddMask
    [Fact]
    public void AddMask_EmptyNull_Returns_EmptyString()
    {
        string emptyString = string.Empty;
        Assert.Equal(emptyString, CnpjFormatter.AddMask(null));
        Assert.Equal(emptyString, CnpjFormatter.AddMask(emptyString));
    }

    [Fact]
    public void AddMask_ValidsCnpjs_Returns_CnpjsWithMask_WithDictionary()
    {
        var testCases = new Dictionary<string, string>
        {
            { "12345678000195", "12.345.678/0001-95" },
            { "98765432000100", "98.765.432/0001-00" },
            { "  11222333000181  ", "11.222.333/0001-81" },
            { "22.333.444/0001-99", "22.333.444/0001-99" },
            { "44455566000177", "44.455.566/0001-77" },
            { "abc12345678000195xyz", "12.345.678/0001-95" },
            { "###98765432000100***", "98.765.432/0001-00" },
        };

        foreach (var (input, expected) in testCases)
        {
            string resultado = CnpjFormatter.AddMask(input);
            Assert.Equal(expected, resultado);
        }
    }

    [Fact]
    public void AddMask_ValidsCnpjsWithMask_Returns_CnpjsWithMask_WithDictionary()
    {
        var testCases = new Dictionary<string, string>
        {
            { "12.345.678/0001-95", "12.345.678/0001-95" },
            { "98.765.432/0001-00", "98.765.432/0001-00" },
            { "11.222.333/0001-81", "11.222.333/0001-81" },
            { "00.000.000/0000-00", "00.000.000/0000-00" },
            { "44.455.566/0001-77", "44.455.566/0001-77" },
            { " 55.666.777/0001-88 ", "55.666.777/0001-88" },
            { "\t66.777.888/0001-99\n", "66.777.888/0001-99" },
        };

        foreach (var (input, expected) in testCases)
        {
            string resultado = CnpjFormatter.AddMask(input);
            Assert.Equal(expected, resultado);
        }
    }
    #endregion

    #region RemoveMask
    [Fact]
    public void RemoveMask_EmptyNull_Returns_EmptyString()
    {
        string emptyString = string.Empty;
        Assert.Equal(emptyString, CnpjFormatter.RemoveMask(null));
        Assert.Equal(emptyString, CnpjFormatter.RemoveMask(emptyString));
        Assert.Equal(emptyString, CnpjFormatter.RemoveMask("   "));
        Assert.Equal(emptyString, CnpjFormatter.RemoveMask("\t\n"));
    }

    [Fact]
    public void RemoveMask_FormattedCnpjs_Returns_OnlyNumbers_WithDictionary()
    {
        var testCases = new Dictionary<string, string>
        {
            { "12.345.678/0001-95", "12345678000195" },
            { "98.765.432/0001-00", "98765432000100" },
            { "11.222.333/0001-81", "11222333000181" },
            { "00.000.000/0000-00", "00000000000000" },
            { "44.455.566/0001-77", "44455566000177" },
            { " 55.666.777/0001-88 ", "55666777000188" },
            { "\t66.777.888/0001-99\n", "66777888000199" },
        };

        foreach (var (input, expected) in testCases)
        {
            string resultado = CnpjFormatter.RemoveMask(input);
            Assert.Equal(expected, resultado);
        }
    }

    [Fact]
    public void RemoveMask_CnpjsWithSpecialCharacters_Returns_OnlyNumbers_WithDictionary()
    {
        var testCases = new Dictionary<string, string>
        {
            { "abc12.345.678/0001-95xyz", "12345678000195" },
            { "###98.765.432/0001-00***", "98765432000100" },
            { "!!11.222.333/0001-81@@", "11222333000181" },
            { "CNPJ: 44.455.566/0001-77", "44455566000177" },
            { "12345678000195", "12345678000195" },
            { "Empresa: 55.666.777/0001-88 - Ativa", "55666777000188" },
            { "   66#777@888$0001%99   ", "66777888000199" },
        };

        foreach (var (input, expected) in testCases)
        {
            string resultado = CnpjFormatter.RemoveMask(input);
            Assert.Equal(expected, resultado);
        }
    }

    [Fact]
    public void RemoveMask_OnlyNumbers_Returns_SameNumbers()
    {
        var testCases = new Dictionary<string, string>
        {
            { "12345678000195", "12345678000195" },
            { "98765432000100", "98765432000100" },
            { "00000000000000", "00000000000000" },
            { "11111111111111", "11111111111111" },
            { "99999999999999", "99999999999999" },
        };

        foreach (var (input, expected) in testCases)
        {
            string resultado = CnpjFormatter.RemoveMask(input);
            Assert.Equal(expected, resultado);
        }
    }

    [Fact]
    public void RemoveMask_InvalidLengthNumbers_Returns_AllNumbers()
    {
        var testCases = new Dictionary<string, string>
        {
            { "123", "123" },
            { "12345", "12345" },
            { "123456789012345", "123456789012345" },
            { "1.2.3.4.5", "12345" },
            { "abc123def456", "123456" },
        };

        foreach (var (input, expected) in testCases)
        {
            string resultado = CnpjFormatter.RemoveMask(input);
            Assert.Equal(expected, resultado);
        }
    }

    [Fact]
    public void RemoveMask_StringWithoutNumbers_Returns_EmptyString()
    {
        var testCases = new[]
        {
            "abcdefghijklmnop",
            "!@#$%^&*()_+",
            "..//--==",
            "CNPJ:",
            "   ",
            "\t\n\r",
        };

        foreach (var input in testCases)
        {
            string resultado = CnpjFormatter.RemoveMask(input);
            Assert.Equal(string.Empty, resultado);
        }
    }

    [Fact]
    public void RemoveMask_RoundTrip_Returns_OriginalNumbers()
    {
        var originalCnpjs = new[]
        {
            "12345678000195",
            "98765432000100",
            "11222333000181",
            "00000000000000",
        };

        foreach (var original in originalCnpjs)
        {
            string withMask = CnpjFormatter.AddMask(original);
            string removedMask = CnpjFormatter.RemoveMask(withMask);
            Assert.Equal(original, removedMask);
        }
    }
    #endregion
}
