using Utilities.Formatters;

namespace TestUtilities.Formatters;

public class PhoneFormatterTest
{
    [Fact]
    public void AddMask_EmptyNull_Returns_EmptyString()
    {
        string emptyString = string.Empty;
        Assert.Equal(emptyString, PhoneFormatter.AddMask(null));
        Assert.Equal(emptyString, PhoneFormatter.AddMask(emptyString));
        Assert.Equal(emptyString, PhoneFormatter.AddMask("   "));
        Assert.Equal(emptyString, PhoneFormatter.AddMask("\t\n"));
    }

    [Fact]
    public void AddMask_EightDigits_Returns_PhoneWithMask_WithDictionary()
    {
        var testCases = new Dictionary<string, string>
        {
            { "12345678", "1234-5678" },
            { "98765432", "9876-5432" },
            { "33334444", "3333-4444" },
            { "11112222", "1111-2222" },
            { "00000000", "0000-0000" },
            { "  12345678  ", "1234-5678" },
            { "abc12345678xyz", "1234-5678" },
            { "###98765432***", "9876-5432" },
        };

        foreach (var (input, expected) in testCases)
        {
            string resultado = PhoneFormatter.AddMask(input);
            Assert.Equal(expected, resultado);
        }
    }

    [Fact]
    public void AddMask_TenDigits_Returns_PhoneWithMask_WithDictionary()
    {
        var testCases = new Dictionary<string, string>
        {
            { "1133334444", "(11) 3333-4444" },
            { "2198765432", "(21) 9876-5432" },
            { "4733331111", "(47) 3333-1111" },
            { "8533334444", "(85) 3333-4444" },
            { "0000000000", "(00) 0000-0000" },
            { "  1133334444  ", "(11) 3333-4444" },
            { "abc1133334444xyz", "(11) 3333-4444" },
            { "###2198765432***", "(21) 9876-5432" },
        };

        foreach (var (input, expected) in testCases)
        {
            string resultado = PhoneFormatter.AddMask(input);
            Assert.Equal(expected, resultado);
        }
    }

    [Fact]
    public void AddMask_ElevenDigits_Returns_CellPhoneWithMask_WithDictionary()
    {
        var testCases = new Dictionary<string, string>
        {
            { "11933334444", "(11) 9 3333-4444" },
            { "21987654321", "(21) 9 8765-4321" },
            { "47999991111", "(47) 9 9999-1111" },
            { "85911112222", "(85) 9 1111-2222" },
            { "00900000000", "(00) 9 0000-0000" },
            { "  11933334444  ", "(11) 9 3333-4444" },
            { "abc11933334444xyz", "(11) 9 3333-4444" },
            { "###21987654321***", "(21) 9 8765-4321" },
        };

        foreach (var (input, expected) in testCases)
        {
            string resultado = PhoneFormatter.AddMask(input);
            Assert.Equal(expected, resultado);
        }
    }

    [Fact]
    public void AddMask_InvalidDigitsCount_Returns_EmptyString()
    {
        var invalidInputs = new[]
        {
            "123",           // 3 dígitos
            "1234567",       // 7 dígitos
            "123456789",     // 9 dígitos
            "123456789012",  // 12 dígitos
            "abcdefghij",    // apenas letras
            "!@#$%^&*()",    // apenas símbolos
            "",              // string vazia
            "   ",           // apenas espaços
        };

        foreach (var input in invalidInputs)
        {
            string resultado = PhoneFormatter.AddMask(input);
            Assert.Equal(string.Empty, resultado);
        }
    }

    [Fact]
    public void AddMask_AlreadyFormattedPhones_Returns_ReformattedPhones()
    {
        var testCases = new Dictionary<string, string>
        {
            { "1234-5678", "1234-5678" },
            { "(11) 3333-4444", "(11) 3333-4444" },
            { "(11) 9 3333-4444", "(11) 9 3333-4444" },
            { " (21) 9876-5432 ", "(21) 9876-5432" },
            { "( 47 ) 9999-1111", "(47) 9999-1111" },
        };

        foreach (var (input, expected) in testCases)
        {
            string resultado = PhoneFormatter.AddMask(input);
            Assert.Equal(expected, resultado);
        }
    }

    [Fact]
    public void RemoveMask_EmptyNull_Returns_EmptyString()
    {
        string emptyString = string.Empty;
        Assert.Equal(emptyString, PhoneFormatter.RemoveMask(null));
        Assert.Equal(emptyString, PhoneFormatter.RemoveMask(emptyString));
        Assert.Equal(emptyString, PhoneFormatter.RemoveMask("   "));
        Assert.Equal(emptyString, PhoneFormatter.RemoveMask("\t\n"));
    }

    [Fact]
    public void RemoveMask_FormattedPhones_Returns_OnlyNumbers_WithDictionary()
    {
        var testCases = new Dictionary<string, string>
        {
            { "1234-5678", "12345678" },
            { "(11) 3333-4444", "1133334444" },
            { "(11) 9 3333-4444", "11933334444" },
            { " (21) 9876-5432 ", "2198765432" },
            { "( 47 ) 9999-1111", "4799991111" },
            { "abc(11)def9ghi3333-4444xyz", "11933334444" },
            { "###(21)***9876-5432###", "2198765432" },
            { "12345678", "12345678" },
            { "11933334444", "11933334444" },
        };

        foreach (var (input, expected) in testCases)
        {
            string resultado = PhoneFormatter.RemoveMask(input);
            Assert.Equal(expected, resultado);
        }
    }

    [Fact]
    public void RemoveMask_SpecialCharacters_Returns_OnlyNumbers()
    {
        var testCases = new Dictionary<string, string>
        {
            { "!@#(11)$%^9&*(3333)-4444", "11933334444" },
            { "++55 11 9 3333-4444", "5511933334444" },
            { " 0xx11 3333.4444 ", "01133334444" },
            { "Tel: (11) 9-3333-4444", "11933334444" },
        };

        foreach (var (input, expected) in testCases)
        {
            string resultado = PhoneFormatter.RemoveMask(input);
            Assert.Equal(expected, resultado);
        }
    }

    [Fact]
    public void AddMask_RoundTrip_Returns_OriginalFormat()
    {
        var originalPhones = new[]
        {
            "1234-5678",
            "(11) 3333-4444",
            "(11) 9 3333-4444"
        };

        foreach (var original in originalPhones)
        {
            string numbersOnly = PhoneFormatter.RemoveMask(original);
            string reformatted = PhoneFormatter.AddMask(numbersOnly);
            Assert.Equal(original, reformatted);
        }
    }
}