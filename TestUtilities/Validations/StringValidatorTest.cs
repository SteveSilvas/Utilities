using Utilities.Validations;

namespace TestUtilities.Validations;

public class StringValidatorTest
{
    [Fact]
    public void IsDigit_NullOrEmptyString_ReturnsFalse()
    {
        string textoVazio = "";

        bool resultadoVazio = StringValidator.IsDigit(textoVazio);

        Assert.False(resultadoVazio);
        Assert.False(StringValidator.IsDigit(null));
    }

    [Fact]
    public void IsDigit_InvalidTextWithLetters_ReturnsFalse_WithList()
    {
        var textosInvalidos = new List<string>
        {
            "123456789as",
            "AS123456789",
            "123456789Sds",
            "123456dsds789",
            "s12s3456789",
            "SCSXSCd123456789",
            "123456789éçãoP",
            "123456789ds00",
            "zero123456789",
            "i123456789",
        };

        foreach (var texto in textosInvalidos)
        {
            bool resultado = StringValidator.IsDigit(texto);
            Assert.False(resultado, $"O texto '{texto}' deveria ser inválido pois não é apenas numérico.");
        }
    }

    [Fact]
    public void IsDigit_InvalidTextWitEspecialCharacteres_ReturnsFalse_WithList()
    {
        var textosInvalidosComEspeciais = new List<string>
        {
            "123!456#789",
            "A1B2C3D4E5F",
            "1a2b3c4d5e6",
            "1.234,567-89",
            "(123)456-7890",
            "[123]456{789}",
            "123 456 789",
            "123_456=789",
            "1+2=3-4*5/6",
            "123$4^5&6*7(8)9",
            "1'2\"3;4:57?",
            "1ç2ã3õ4ú5é6í7à8",
            "1\t2\n3\r4\f5\b6",
            "123\0456\a789",
            "~!@#$%¨&*()_+",
            "´`[{ª]}º\\|;",
            ",.<>/?':\"",
            "1²2³3£4¢5¬6§7ª",
            "¹²³¼½¾",
            "¹₂₃₄₅₆"
        };

        foreach (var texto in textosInvalidosComEspeciais)
        {
            bool resultado = StringValidator.IsDigit(texto);
            Assert.False(resultado, $"O texto '{texto}' deveria ser inválido pois não é apenas numérico.");
        }
    }

    [Fact]
    public void IsDigit_ValidNumbers_ReturnsTrue_WithList()
    {
        var textosApenasNumeros = new List<string>
        {
            "123456789",
            "00000000000",
            "9876543210",
            "1",
            "0",
            "102030405",
            "999888777",
            "543210987",
            "11223344556",
            "90123456789"
        };

        foreach (var texto in textosApenasNumeros)
        {
            bool resultado = StringValidator.IsDigit(texto);
            Assert.True(resultado, $"O texto '{texto}' deveria ser válido pois é numérico.");
        }
    }
}