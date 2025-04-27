using Utilities.Validations;

namespace TestUtilities.Validations
{
    public class CpfValidatorTest
    {
        [Fact]
        public void IsCpf_NullOrEmptyCpf_ReturnsFalse()
        {
            string cpfVazio = "";

            bool resultadoVazio = CpfValidator.IsCpf(cpfVazio);

            Assert.False(resultadoVazio);
            Assert.False(CpfValidator.IsCpf(null));
        }

        [Fact]
        public void IsCpf_InvalidCpf_ReturnsFalse()
        {
            string cpfInvalido1 = "11111111111";
            string cpfInvalido2 = "12345678900";
            string cpfInvalidoComLetra = "1234567890a";
            string cpfMuitoCurto = "1234567890";
            string cpfMuitoLongo = "123456789091";

            bool resultado1 = CpfValidator.IsCpf(cpfInvalido1);
            bool resultado2 = CpfValidator.IsCpf(cpfInvalido2);
            bool resultadoComLetra = CpfValidator.IsCpf(cpfInvalidoComLetra);
            bool resultadoCurto = CpfValidator.IsCpf(cpfMuitoCurto);
            bool resultadoLongo = CpfValidator.IsCpf(cpfMuitoLongo);

            Assert.False(resultado1);
            Assert.False(resultado2);
            Assert.False(resultadoComLetra);
            Assert.False(resultadoCurto);
            Assert.False(resultadoLongo);
        }

        [Fact]
        public void IsCpf_ValidCpf_ReturnsTrue_WithList()
        {
            var cpfsValidos = new List<string>
            {
                "231.871.340-50",
                "653.734.390-96",
                "123.456.789-09",
                "518.601.860-09",
                "047.696.370-20",
                "541.640.260-14",
                "431.616.560-75",
                "443.591.880-30",
                "950.673.970-63",
                "312.973.260-83"
            };

            foreach (var cpf in cpfsValidos)
            {
                bool resultado = CpfValidator.IsCpf(cpf);
                Assert.True(resultado, $"CPF '{cpf}' deveria ser válido.");
            }
        }
    }
}