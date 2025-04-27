using Utilities.Validations;

namespace TestUtilities.Converters
{
    public class StringConversorTest
    {
        [Fact]
        public void OnlyNumbers_StringWithNumbersAndCharacteres_ReturnsNumbers_WithList()
        {
            // Arrange
            var listaMisturada = new List<string>
            {
                "234431;f@$54*&5648786",
                "7658/64f845dd6756yu@T75",
                "'0]",
                "46755467467567567567"
            };

            var listaDigitos = new List<string>
            {
                "234431545648786",
                "765864845675675",
                "0",
                "46755467467567567567"
            };

            // Act & Assert
            for (int i = 0; i < listaMisturada.Count; i++)
            {
                string texto = listaMisturada[i];
                string textoNumerico = StringConversor.OnlyNumbers(texto);
                Assert.Equal(textoNumerico, listaDigitos[i]);
            }
        }
    }
}
