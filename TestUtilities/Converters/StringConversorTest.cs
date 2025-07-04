using Utilities.Validations;

namespace TestUtilities.Converters
{
    public class StringConversorTest
    {
        #region Only Numbers Tests
        [Fact]
        public void OnlyNumbers_NullOrEmpty_ReturnsEmptyString()
        {
            Assert.Equal(string.Empty, StringConversor.OnlyNumbers(null));
            Assert.Equal(string.Empty, StringConversor.OnlyNumbers(""));
        }

        [Fact]
        public void OnlyNumbers_StringWithOnlyNumbers_ReturnsSameString()
        {
            Assert.Equal("123456", StringConversor.OnlyNumbers("123456"));
        }

        [Fact]
        public void OnlyNumbers_StringWithSpecialChars_ReturnsOnlyNumbers()
        {
            Assert.Equal("987654321", StringConversor.OnlyNumbers("9-8(7)6_5+4=3 2@1!"));
        }

        [Fact]
        public void OnlyNumbers_StringWithNumbersAndCharacteres_ReturnsNumbers_WithList()
        {
            var listaMisturada = new List<string>
            {
                "234431;f@$54*&5648786",
                "7658/64f845dd6756yu@T75",
                "'0]vff",
                "46755467467567fddfd567567"
            };

            var listaDigitos = new List<string>
            {
                "234431545648786",
                "765864845675675",
                "0",
                "46755467467567567567"
            };

            for (int i = 0; i < listaMisturada.Count; i++)
            {
                string texto = listaMisturada[i];
                string textoNumerico = StringConversor.OnlyNumbers(texto);
                Assert.Equal(textoNumerico, listaDigitos[i]);
            }
        }

        [Fact]
        public void OnlyNumbers_StringWithUnicodeNumbers_ReturnsEmpty()
        {
            Assert.Equal("", StringConversor.OnlyNumbers("Ⅷ Ⅸ ٠١٢٣٤٥٦٧٨٩ ०१२३४५६७८९ "));
        }
        #endregion


        #region Only Letters Tests
        [Fact]
        public void OnlyLetters_NullOrEmpty_ReturnsEmptyString()
        {
            Assert.Equal(string.Empty, StringConversor.OnlyLetters(null));
            Assert.Equal(string.Empty, StringConversor.OnlyLetters(""));
        }

        [Fact]
        public void OnlyLetters_StringWithNumbersAndCharacteres_ReturnsLetters_WithList()
        {
            var listaMisturada = new List<string>
            {
                "234431;f@$54*&5648786",
                "7658/64f845dd6756yu@T75",
                "'0]vff",
                "46755467467567fddfd567567"
            };

            var listaLetras = new List<string>
            {
                "f",
                "fddyuT",
                "vff",
                "fddfd"
            };

            for (int i = 0; i < listaMisturada.Count; i++)
            {
                string texto = listaMisturada[i];
                string textoEmLetras = StringConversor.OnlyLetters(texto);
                Assert.Equal(textoEmLetras, listaLetras[i]);
            }
        }
        #endregion


        #region RemoveDiacritics Tests
        [Fact]
        public void RemoveDiacritics_NullOrEmpty_ReturnsEmptyString()
        {
            Assert.Equal(string.Empty, StringConversor.RemoveDiacritics(null));
            Assert.Equal(string.Empty, StringConversor.RemoveDiacritics(""));
        }

        [Fact]
        public void RemoveDiacritics_StringWithWhitespace_PreservesWhitespace()
        {
            Assert.Equal(" hello world ", StringConversor.RemoveDiacritics(" hello world "));
            Assert.Equal("   ", StringConversor.RemoveDiacritics("   "));
            Assert.Equal("teste de espacos", StringConversor.RemoveDiacritics("teste de espacos"));
            Assert.Equal("A B C", StringConversor.RemoveDiacritics("A B C"));
            Assert.Equal("  Sao Paulo  ", StringConversor.RemoveDiacritics("  São Paulo  "));
        }

        [Fact]
        public void RemoveDiacritics_StringWithoutAccents_ReturnsSameString()
        {
            string input = "hello world";
            string expected = "hello world";

            Assert.Equal(expected, StringConversor.RemoveDiacritics(input));
        }

        [Fact]
        public void RemoveDiacritics_StringWithLatinAccents_ReturnsWithoutAccents()
        {
            var testCases = new Dictionary<string, string>
            {
                {"café", "cafe"},
                {"naïve", "naive"},
                {"résumé", "resume"},
                {"piñata", "pinata"},
                {"Björk", "Bjork"},
                {"Müller", "Muller"},
                {"São Paulo", "Sao Paulo"},
                {"João", "Joao"},
                {"coração", "coracao"},
                {"Ação", "Acao"}
            };

            foreach (var testCase in testCases)
            {
                Assert.Equal(testCase.Value, StringConversor.RemoveDiacritics(testCase.Key));
            }
        }

        [Fact]
        public void RemoveDiacritics_StringWithVariousLanguageAccents_ReturnsWithoutAccents()
        {
            var testCases = new Dictionary<string, string>
            {
                // Português
                {"ção", "cao"},
                {"mãe", "mae"},
                {"pão", "pao"},
        
                // Francês
                {"être", "etre"},
                {"naïf", "naif"},
                {"noël", "noel"},
        
                // Alemão
                {"Mädchen", "Madchen"},
                {"Größe", "Grosse"},
        
                // Espanhol
                {"niño", "nino"},
                {"señor", "senor"},
        
                // Italiano
                {"perché", "perche"},
                {"città", "citta"}
            };

            foreach (var testCase in testCases)
            {
                Assert.Equal(testCase.Value, StringConversor.RemoveDiacritics(testCase.Key));
            }
        }

        [Fact]
        public void RemoveDiacritics_StringWithCapitalizedSpecialCharacters_ReturnsCorrectly()
        {
            Assert.Equal("GROSSE", StringConversor.RemoveDiacritics("GRÖSSE"));
            Assert.Equal("SAO PAULO", StringConversor.RemoveDiacritics("SÃO PAULO"));
            Assert.Equal("JOAO", StringConversor.RemoveDiacritics("JOÃO"));
            Assert.Equal("CUIDADO COM A COBRANCA DO PRECO", StringConversor.RemoveDiacritics("CUIDADO COM A COBRANÇA DO PREÇO"));
            Assert.Equal("AEIOU", StringConversor.RemoveDiacritics("ÁÉÍÓÚ"));
            Assert.Equal("AEIOU", StringConversor.RemoveDiacritics("ÀÈÌÒÙ"));
            Assert.Equal("AEIOU", StringConversor.RemoveDiacritics("ÂÊÎÔÛ"));
        }

        [Fact]
        public void RemoveDiacritics_StringWithCyrillicOrGreekCharacters_RemovesTheirDiacritics()
        {
            Assert.Equal("Привіт світ", StringConversor.RemoveDiacritics("Привіт світ"));

            Assert.Equal("Γεια σου κοσμε", StringConversor.RemoveDiacritics("Γειά σου κόσμε"));
        }
        #endregion


        #region Only SLugfy Tests
        [Fact]
        public void Slugfy_NullOrEmpty_ReturnsEmptyString()
        {
            Assert.Equal(string.Empty, StringConversor.Slugify(null));
            Assert.Equal(string.Empty, StringConversor.Slugify(""));
        }

        [Fact]
        public void Slugify_StringWithUnicodeLetters_ReturnsEmpty()
        {
            string input = "こんにちは 世界";

            Assert.Equal(string.Empty, StringConversor.Slugify(input));
        }

        [Fact]
        public void Slugify_StringWithOnlySpacesAndHyphens_ReturnsEmpty()
        {
            string input = "   ---   ---   ";

            Assert.Equal(string.Empty, StringConversor.Slugify(input));
        }

        [Fact]
        public void Slugify_StringWithNumbersAndInvalidChars_ReturnsNumbers()
        {
            string input = "123!@#456";
            string expected = "123456";

            Assert.Equal(expected, StringConversor.Slugify(input));
        }

        [Fact]
        public void Slugify_StringWithOnlySpecialChars_ReturnsEmpty()
        {
            string input = "!@#$%^&*(){}[]|\\:;\"'<>,.?/~`";

            Assert.Equal(string.Empty, StringConversor.Slugify(input));
        }

        [Fact]
        public void Slugify_StringWithMultipleSpacesAndHyphens_ReturnsCleanSlug()
        {
            string input = "  hello   ---   world  ";
            string expected = "hello-world";

            Assert.Equal(expected, StringConversor.Slugify(input));
        }

        [Fact]
        public void Slugify_StringWithAccentsAndSpecialChars_ReturnsSlug()
        {
            string input = "Olá, Mundo! C# é demais.";
            string expected = "ola-mundo-c-e-demais";

            Assert.Equal(expected, StringConversor.Slugify(input));
        }

        [Fact]
        public void Slugfy_StringWithNumbersAndCharacteres_ReturnsTextUrl_WithList()
        {
            var inputs = new List<string>
            {
                "Testando 123, com vírgulas e pontos...",
                "Café com leite - manhã ensolarada!",
                "API RESTful: versão 2.0 (beta)",
                "Espaços     múltiplos e - hífens--duplicados",
                "Código #CSharp é legal!",
                "   leading and trailing spaces    ",
                "Símbolos @#$%&*() devem sumir",
                "Números 1234567890 ficam",
                "Mix de CAPS e minúsculas",
                "Texto com ç, ã, ê, ó, û e outros"
            };

            var expected = new List<string>
            {
                "testando-123-com-virgulas-e-pontos",
                "cafe-com-leite-manha-ensolarada",
                "api-restful-versao-20-beta",
                "espacos-multiplos-e-hifens-duplicados",
                "codigo-csharp-e-legal",
                "leading-and-trailing-spaces",
                "simbolos-devem-sumir",
                "numeros-1234567890-ficam",
                "mix-de-caps-e-minusculas",
                "texto-com-c-a-e-o-u-e-outros"
            };

            for (int i = 0; i < inputs.Count; i++)
            {
                var resultado = StringConversor.Slugify(inputs[i]);
                Assert.Equal(expected[i], resultado);
            }
        }

        [Fact]
        public void Slugify_VeryLongString_ReturnsValidSlug()
        {
            string input = "Este é um texto muito longo que precisa ser testado para verificar se a funcionalidade de slug funciona corretamente com strings extensas que podem ter muitos caracteres especiais e espaços";
            string expected = "este-e-um-texto-muito-longo-que-precisa-ser-testado-para-verificar-se-a-funcionalidade-de-slug-funciona-corretamente-com-strings-extensas-que-podem-ter-muitos-caracteres-especiais-e-espacos";

            Assert.Equal(expected, StringConversor.Slugify(input));
        }


        [Fact]
        public void Slugify_StringWithConsecutiveSpecialChars_ReturnsCleanSlug()
        {
            string input = "hello!!!@@##world";
            string expected = "helloworld";

            Assert.Equal(expected, StringConversor.Slugify(input));
        }

        [Fact]
        public void Slugify_StringWithMixedCaseAndNumbers_ReturnsLowerCaseSlug()
        {
            string input = "API-V2.1-BETA-2023";
            string expected = "api-v21-beta-2023";

            Assert.Equal(expected, StringConversor.Slugify(input));
        }

        [Fact]
        public void Slugify_StringWithUnderscores_ReturnsSlugWithHyphens()
        {
            string input = "my_awesome_function_name";
            string expected = "my-awesome-function-name";

            Assert.Equal(expected, StringConversor.Slugify(input));
        }

        [Fact]
        public void Slugify_StringWithTabsAndNewlines_ReturnsCleanSlug()
        {
            string input = "hello\tworld\nfrom\r\ncode";
            string expected = "hello-world-from-code";

            Assert.Equal(expected, StringConversor.Slugify(input));
        }

        [Fact]
        public void Slugify_StringWithQuotes_ReturnsSlugWithoutQuotes()
        {
            string input = "\"Hello 'World'\" said the 'Developer'";
            string expected = "hello-world-said-the-developer";

            Assert.Equal(expected, StringConversor.Slugify(input));
        }

        [Fact]
        public void Slugify_StringWithEmailFormat_ReturnsValidSlug()
        {
            string input = "user@example.com";
            string expected = "userexamplecom";

            Assert.Equal(expected, StringConversor.Slugify(input));
        }

        [Fact]
        public void Slugify_StringWithUrlFormat_ReturnsValidSlug()
        {
            string input = "https://www.example.com/path?param=value";
            string expected = "httpswwwexamplecompathparamvalue";

            Assert.Equal(expected, StringConversor.Slugify(input));
        }

        [Fact]
        public void Slugify_StringWithParenthesesAndBrackets_ReturnsCleanSlug()
        {
            string input = "Method(param1, param2) [deprecated]";
            string expected = "methodparam1-param2-deprecated";

            Assert.Equal(expected, StringConversor.Slugify(input));
        }

        [Fact]
        public void Slugify_StringWithDots_ReturnsSlugWithHyphens()
        {
            string input = "version.1.2.3.release";
            string expected = "version123release";

            Assert.Equal(expected, StringConversor.Slugify(input));
        }

        [Fact]
        public void Slugify_StringWithEmojis_ReturnsSlugWithoutEmojis()
        {
            string input = "Hello 😊 World 🚀 Test";
            string expected = "hello-world-test";

            Assert.Equal(expected, StringConversor.Slugify(input));
        }

        [Fact]
        public void Slugify_StringWithCurrency_ReturnsSlugWithoutCurrencySymbols()
        {
            string input = "Price: $100.50 €75.30 ¥1000";
            string expected = "price-10050-7530-1000";

            Assert.Equal(expected, StringConversor.Slugify(input));
        }

        [Fact]
        public void Slugify_StringWithDifferentLanguageAccents_ReturnsNormalizedSlug()
        {
            var inputs = new List<string>
            {
                "Café français",           // French
                "Niño español",           // Spanish
                "Straße deutsch",         // German
                "Ragazzo italiano",       // Italian
                "Dziecko polskie"         // Polish
            };

            var expected = new List<string>
            {
                "cafe-francais",
                "nino-espanol",
                "strasse-deutsch",
                "ragazzo-italiano",
                "dziecko-polskie"
            };

            for (int i = 0; i < inputs.Count; i++)
            {
                var resultado = StringConversor.Slugify(inputs[i]);
                Assert.Equal(expected[i], resultado);
            }
        }
        #endregion
    }
}
