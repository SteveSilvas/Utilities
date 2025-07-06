using Utilities.Validations;

namespace TestUtilities.Validations;

public class EmailValidatorTest
{
    [Fact]
    public void IsValid_ValidEmails_ReturnsTrue_WithList()
    {
        var emailsValidos = new List<string>
        {
            "teste@example.com",
            "usuario.teste@subdominio.example.com.br",
            "email+com.sinal@example.net",
            "12345@meudominio.info",
            "teste_sublinhado@example-servidor.com",
            "primeironome.ultimonome@algumacoisa.org",
            "a@b.cd",
            "muito.longo.nome.de.usuario@dominio.com",
            "emailcomcaracteresespeciais!#$%&'*+-/=?^_`{|}~@dominio.info",
            "letramaiuscula@Example.com"
        };

        foreach (var email in emailsValidos)
        {
            bool resultado = EmailValidator.IsValid(email);
            Assert.True(resultado, $"O email: '{email}' deveria ser válido.");
        }
    }

    [Fact]
    public void IsValid_InvalidEmails_ReturnsFalse_WithList()
    {
        var emailsInvalidos = new List<string?>
        {
            "teste",
            "teste@",
            "@example.com",
            "teste@example",
            "teste@.com",
            "teste@example.",
            "teste..usuario@example.com",
            ".teste@example.com",
            "teste@example_.com",
            "teste@-example.com",
            "teste@example-.com",
            "teste@example..com",
            "teste@123.123.123.1234",
            "teste@example com espaço.com",
            "teste@example!com",
            "muitooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo.longo@example.com",
            "teste@muitooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo.longo.com",
            null,
            "teste@example.c"
        };

        foreach (var email in emailsInvalidos)
        {
            bool resultado = EmailValidator.IsValid(email);
            Assert.False(resultado, $"O email: '{email}' deveria ser inválido.");
        }
    }
}
