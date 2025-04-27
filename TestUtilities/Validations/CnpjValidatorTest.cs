using Utilities.Validations;

namespace TestUtilities.Validations
{
    public class CnpjValidatorTest
    {
        [Fact]
        public void IsCnpj_NullOrEmptyCnpj_ReturnsFalse()
        {
            string cnpjVazio = "";

            bool resultadoVazio = CnpjValidator.IsCnpj(cnpjVazio);

            Assert.False(resultadoVazio);
            Assert.False(CnpjValidator.IsCnpj(null));
        }

        [Fact]
        public void IsCnpj_InvalidCnpj_ReturnsFalse()
        {
            string cnpjInvalido1 = "51.708.892/0001-99";
            string cnpjInvalido2 = "51.708.892/0001-17";
            string cnpjInvalidoComLetra = "51.708.892/0001-10as";
            string cnpjMuitoCurto = "51.708.892/0001";
            string cnpjMuitoLongo = "51.708.892/0001-1032";

            bool resultado1 = CnpjValidator.IsCnpj(cnpjInvalido1);
            bool resultado2 = CnpjValidator.IsCnpj(cnpjInvalido2);
            bool resultadoComLetra = CnpjValidator.IsCnpj(cnpjInvalidoComLetra);
            bool resultadoCurto = CnpjValidator.IsCnpj(cnpjMuitoCurto);
            bool resultadoLongo = CnpjValidator.IsCnpj(cnpjMuitoLongo);

            Assert.False(resultado1);
            Assert.False(resultado2);
            Assert.False(resultadoComLetra);
            Assert.False(resultadoCurto);
            Assert.False(resultadoLongo);
        }

        [Fact]
        public void IsCpf_ValidCpf_ReturnsTrue_WithList()
        {
            var cnpjsValidos = new List<string>
            {
                "97118284000157",
                "49577095000108",
                "40.568.691/0001-49",
                "32.436.219/0001-24",
                "15.977.722/0001-78",
                "28.307.698/0001-01",
                "13.325.681/0001-00",
                "66.305.430/0001-20",
                "64.531.201/0001-06",
                "51.708.892/0001-10"
            };

            foreach (var cnpj in cnpjsValidos)
            {
                bool resultado = CnpjValidator.IsCnpj(cnpj);
                Assert.True(resultado, $"CNPJ '{cnpj}' deveria ser válido.");
            }
        }
    }
}
