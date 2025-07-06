using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilities.Validations
{
    /// <summary>
    /// Classe utilitária para realizar conversões e sanitizações de strings.
    /// Contém métodos para extrair apenas números, letras, gerar slugs e remover diacríticos.
    /// </summary>
    public static class StringConversor
    {
        /// <summary>
        /// Remove todos os caracteres não numéricos de uma string.
        /// </summary>
        /// <param name="input">Texto de entrada a ser processado.</param>
        /// <returns>
        /// Uma string contendo apenas os dígitos numéricos (0-9) do texto de entrada.
        /// Retorna uma string vazia se o parâmetro for nulo ou vazio.
        /// </returns>
        public static string OnlyNumbers(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return new string(input.Where(c => char.IsAsciiDigit(c)).ToArray());
        }

        /// <summary>
        /// Remove todos os caracteres não alfabéticos de uma string.
        /// </summary>
        /// <param name="input">Texto de entrada a ser processado.</param>
        /// <returns>
        /// Uma string contendo apenas as letras (a-z, A-Z) do texto de entrada.
        /// Retorna uma string vazia se o parâmetro for nulo ou vazio.
        /// </returns>
        public static string OnlyLetters(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return new string(input.Where(char.IsLetter).ToArray());
        }

        /// <summary>
        /// Converte uma string em um formato "slug".
        /// Remove acentos, caracteres especiais e substitui espaços e sublinhados por hífens.
        /// </summary>
        /// <param name="input">Texto de entrada a ser convertido em slug.</param>
        /// <returns>
        /// Um slug em letras minúsculas e separado por hífens.
        /// Retorna uma string vazia se o parâmetro for nulo ou conter apenas espaços.
        /// </returns>
        public static string Slugify(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            string result = input.ToLowerInvariant();

            // Substituições específicas para idiomas
            result = result.Replace("ß", "ss"); // Alemão
            result = result.Replace("æ", "ae"); // Norueguês/Dinamarquês
            result = result.Replace("ø", "o");  // Norueguês/Dinamarquês
            result = result.Replace("å", "aa"); // Norueguês/Dinamarquês

            result = RemoveDiacritics(result);  // Remove acentos
            result = result.Replace("_", "-");  // Substitui sublinhados por hífens

            // Remove caracteres especiais restantes
            result = Regex.Replace(result, @"[^a-z0-9\s-]", "");

            // Substitui múltiplos espaços ou hífens por um único hífen
            result = Regex.Replace(result, @"[\s-]+", "-").Trim('-');

            return result;
        }

        /// <summary>
        /// Remove diacríticos (acentos) e caracteres especiais de uma string.
        /// Converte caracteres compostos em seus equivalentes básicos.
        /// </summary>
        /// <param name="text">Texto de entrada a ser normalizado.</param>
        /// <returns>
        /// Uma string sem diacríticos. Retorna uma string vazia se o parâmetro for nulo ou vazio.
        /// </returns>
        public static string RemoveDiacritics(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var sb = new StringBuilder(text);

            // Substituições específicas para idiomas
            sb.Replace("ß", "ss");
            sb.Replace("ẞ", "SS");
            sb.Replace("æ", "ae");
            sb.Replace("ø", "o");
            sb.Replace("å", "aa");

            string processedText = sb.ToString();

            // Normaliza para decompor caracteres acentuados
            var normalized = processedText.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder();

            foreach (var c in normalized)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(c);
                }
            }

            return builder
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }
    }
}
