using Xunit;
using Utilities.Validations;

namespace TestUtilities.Validations
{
    public class CpfValidatorTest
    {
        [Fact]
        public void IsCpf_ValidCpf_ReturnsTrue_WithList()
        {
            // Arrange
            var cpfsValidos = new List<string>
            {
                "12345678909",
                "42411273800",
                "123.456.789-09",
                "424.112.738-00"
            };

            // Act & Assert
            foreach (var cpf in cpfsValidos)
            {
                bool resultado = CpfValidator.IsCpf(cpf);
                Assert.True(resultado, $"CPF '{cpf}' deveria ser válido.");
            }
        }

        [Fact]
        public void IsCpf_InvalidCpf_ReturnsFalse()
        {
            // Arrange
            string cpfInvalido1 = "11111111111";
            string cpfInvalido2 = "12345678900";
            string cpfInvalidoComLetra = "1234567890a";
            string cpfMuitoCurto = "1234567890";
            string cpfMuitoLongo = "123456789091";

            // Act
            bool resultado1 = CpfValidator.IsCpf(cpfInvalido1);
            bool resultado2 = CpfValidator.IsCpf(cpfInvalido2);
            bool resultadoComLetra = CpfValidator.IsCpf(cpfInvalidoComLetra);
            bool resultadoCurto = CpfValidator.IsCpf(cpfMuitoCurto);
            bool resultadoLongo = CpfValidator.IsCpf(cpfMuitoLongo);

            // Assert
            Assert.False(resultado1);
            Assert.False(resultado2);
            Assert.False(resultadoComLetra);
            Assert.False(resultadoCurto);
            Assert.False(resultadoLongo);
        }

        [Fact]
        public void IsCpf_EmptyCpf_ReturnsFalse()
        {
            // Arrange
            string cpfVazio = "";

            // Act
            bool resultadoVazio = CpfValidator.IsCpf(cpfVazio);

            // Assert
            Assert.False(resultadoVazio);
        }
    }
}