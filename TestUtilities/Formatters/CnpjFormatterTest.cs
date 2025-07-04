using Utilities.Formatters;

namespace TestUtilities.Formatters
{
    public class CnpjFormatterTest
    {
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
    }
}
