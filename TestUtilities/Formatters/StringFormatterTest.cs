using Utilities.Formatters;

namespace TestUtilities.Formatters;

public class StringFormatterTest
{
    #region CleanString
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
        }
        ;
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
    #endregion

    #region NormalizeSpaces
    [Fact]
    public void NormalizeSpaces_emptyOrNull_ReturnsEmptyString()
    {
        var valuesList = new List<string?>
        {
            null,
            "",
            " ",
            "  ",
            "   ",
            "    "
        };

        foreach (string? item in valuesList)
        {
            string result = StringFormatter.NormalizeSpaces(item);

            Assert.Equal(string.Empty, result);
        }
        ;
    }

    [Fact]
    public void NormalizeSpaces_textWithSpaces_ReturnsOneSpaceBetweenItems()
    {
        var testCases = new Dictionary<string, string>
            {
                { "alfa    beto", "alfa beto" },
                { "Para   ra para", "Para ra para" },
                { "   texto com espaco no começo", "texto com espaco no começo" },
                { "   entre espaços  ", "entre espaços" },

            };

        foreach (var (input, expected) in testCases)
        {
            string resultado = StringFormatter.NormalizeSpaces(input);
            Assert.Equal(expected, resultado);
        }
        ;
    }
    #endregion

    #region ToTitleCase
    [Fact]
    public void ToTitleCase_emptyOrNull_ReturnsEmptyString()
    {
        var valuesList = new List<string?> { null, "", "   " };

        foreach (string? item in valuesList)
        {
            string result = StringFormatter.ToTitleCase(item);
            Assert.Equal(string.Empty, result);
        }
    }

    [Fact]
    public void ToTitleCase_text_ReturnsTitleCasedText()
    {
        var testCases = new Dictionary<string, string>
    {
        { "hello world", "Hello World" },
        { "tESTE de TEXTO", "Teste De Texto" },
        { "já está certo", "Já Está Certo" }
    };

        foreach (var (input, expected) in testCases)
        {
            string result = StringFormatter.ToTitleCase(input);
            Assert.Equal(expected, result);
        }
    }
    #endregion

    #region CapitalizeFirstLetter
    [Fact]
    public void CapitalizeFirstLetter_emptyOrNull_ReturnsEmptyString()
    {
        var valuesList = new List<string?> { null, "", "   " };

        foreach (string? item in valuesList)
        {
            string result = StringFormatter.CapitalizeFirstLetter(item);
            Assert.Equal(string.Empty, result);
        }
    }

    [Fact]
    public void CapitalizeFirstLetter_text_ReturnsCapitalizedText()
    {
        var testCases = new Dictionary<string, string>
    {
        { "texto", "Texto" },
        { "TEXTO", "Texto" },
        { "tExTo", "Texto" },
        { "éxemplo", "Éxemplo" }
    };

        foreach (var (input, expected) in testCases)
        {
            string result = StringFormatter.CapitalizeFirstLetter(input);
            Assert.Equal(expected, result);
        }
    }
    #endregion

    #region RemoveSpecialCharacters
    [Fact]
    public void RemoveSpecialCharacters_emptyOrNull_ReturnsEmptyString()
    {
        var valuesList = new List<string?> { null, "", "   " };

        foreach (string? item in valuesList)
        {
            string result = StringFormatter.RemoveSpecialCharacters(item);
            Assert.Equal(string.Empty, result);
        }
    }

    [Fact]
    public void RemoveSpecialCharacters_textWithSpecials_RemovesSpecialCharacters()
    {
        var testCases = new Dictionary<string, string>
    {
        { "abc123", "abc123" },
        { "abc-123", "abc123" },
        { "áéíóúç!@#", "áéíóúç" },
        { "ABC_def$%^", "ABCdef" }
    };

        foreach (var (input, expected) in testCases)
        {
            string result = StringFormatter.RemoveSpecialCharacters(input);
            Assert.Equal(expected, result);
        }
    }
    #endregion

    #region ReplaceMultiple
    [Fact]
    public void ReplaceMultiple_emptyOrNull_ReturnsEmptyString()
    {
        var replacements = new Dictionary<string, string> { { "a", "b" } };

        var valuesList = new List<string?> { null, "", "   " };

        foreach (string? item in valuesList)
        {
            string result = StringFormatter.ReplaceMultiple(item, replacements);
            Assert.Equal(string.Empty, result);
        }
    }

    [Fact]
    public void ReplaceMultiple_textWithReplacements_ReturnsReplacedText()
    {
        var replacements = new Dictionary<string, string>
    {
        { "foo", "bar" },
        { "123", "456" }
    };

        var testCases = new Dictionary<string, string>
    {
        { "foo and 123", "bar and 456" },
        { "no match here", "no match here" },
        { "foofoo123", "barbar456" }
    };

        foreach (var (input, expected) in testCases)
        {
            string result = StringFormatter.ReplaceMultiple(input, replacements);
            Assert.Equal(expected, result);
        }
    }
    #endregion

    #region LimitLength
    [Fact]
    public void LimitLength_emptyOrNull_ReturnsEmptyString()
    {
        var valuesList = new List<string?> { null, "", "   " };

        foreach (string? item in valuesList)
        {
            string result = StringFormatter.LimitLength(item, 10);
            Assert.Equal(string.Empty, result);
        }
    }

    [Fact]
    public void LimitLength_textWithinLimit_ReturnsSameText()
    {
        string input = "short";
        string result = StringFormatter.LimitLength(input, 10);
        Assert.Equal("short", result);
    }

    [Fact]
    public void LimitLength_textExceedingLimit_ReturnsTruncatedText()
    {
        string input = "This is a long text";
        string result = StringFormatter.LimitLength(input, 10);
        Assert.Equal("This is a...", result);
    }
    #endregion

    #region SanitizeHtml
    [Fact]
    public void SanitizeHtml_nullOrEmpty_ReturnsEmptyString()
    {
        var valuesList = new List<string?> { null, "", "   " };

        foreach (string? item in valuesList)
        {
            string result = StringFormatter.SanitizeHtml(item);
            Assert.Equal(string.Empty, result);
        }
    }

    [Fact]
    public void SanitizeHtml_textWithoutHtml_ReturnsSameText()
    {
        string input = "This is plain text";
        string result = StringFormatter.SanitizeHtml(input);
        Assert.Equal("This is plain text", result);
    }

    [Fact]
    public void SanitizeHtml_textWithHtml_RemovesTags()
    {
        var testCases = new Dictionary<string, string>
        {
            { "<p>Hello <b>World</b>!</p>", "Hello World!" },
            { "<div>Test<br/>Line</div>", "TestLine" },
            { "<script>alert('hack');</script>Safe", "Safe" },
            { "<style>.class { color:red; }</style>Styled", "Styled" },
            { "<a href='link'>Click</a> here", "Click here" },
            { "&lt;div&gt;Encoded&lt;/div&gt;", "Encoded" },
            { "<!-- Comment -->Visible", "Visible" },
        };

        foreach (var (input, expected) in testCases)
        {
            string result = StringFormatter.SanitizeHtml(input);
            Assert.Equal(expected, result);
        }
    }

    [Fact]
    public void SanitizeHtml_textWithHtmlEntities_DecodesEntities()
    {
        string input = "AT&amp;T &lt;Company&gt;";
        string result = StringFormatter.SanitizeHtml(input);
        Assert.Equal("AT&T", result);
    }
    #endregion
}
