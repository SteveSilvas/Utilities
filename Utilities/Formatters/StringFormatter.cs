using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilities.Formatters
{
    /// <summary>
    /// Classe utilitária para formatação de strings.
    /// Contém métodos para limpeza e padronização de textos.
    /// </summary>
    public class StringFormatter
    {
        /// <summary>
        /// Remove espaços em branco no início e no final de uma string.
        /// </summary>
        /// <param name="text">Texto de entrada a ser limpo.</param>
        /// <returns>
        /// Uma string sem espaços no início e no fim.  
        /// Retorna uma string vazia se o parâmetro for nulo ou vazio.
        /// </returns>
        public static string CleanSpaces(string? text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            return text.Trim();
        }

        /// <summary>
        /// Remove espaços duplicados em uma string e mantém apenas um espaço entre palavras.
        /// </summary>
        /// <param name="text">Texto de entrada a ser normalizado.</param>
        /// <returns>
        /// Uma string com espaçamento normalizado. Retorna uma string vazia se o parâmetro for nulo ou vazio.
        /// </returns>
        public static string NormalizeSpaces(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            return Regex.Replace(text.Trim(), @"\s+", " ");
        }

        /// <summary>
        /// Converte a string para o formato Title Case (todas as palavras começam com letra maiúscula).
        /// </summary>
        /// <param name="text">Texto de entrada.</param>
        /// <returns>
        /// A string formatada em Title Case. Retorna uma string vazia se o parâmetro for nulo ou vazio.
        /// </returns>
        public static string ToTitleCase(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
        }

        /// <summary>
        /// Capitaliza apenas a primeira letra da string.
        /// </summary>
        /// <param name="text">Texto de entrada.</param>
        /// <returns>
        /// String com a primeira letra maiúscula e o restante minúsculo.
        /// </returns>
        public static string CapitalizeFirstLetter(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            return char.ToUpper(text[0]) + text.Substring(1).ToLower();
        }

        /// <summary>
        /// Remove todos os caracteres especiais de uma string, mantendo apenas letras e números.
        /// </summary>
        /// <param name="text">Texto de entrada.</param>
        /// <returns>
        /// Uma string contendo apenas letras e números. Retorna string vazia se a entrada for nula ou vazia.
        /// </returns>
        public static string RemoveSpecialCharacters(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            return Regex.Replace(text, @"[^a-zA-Z0-9]", "");
        }

        /// <summary>
        /// Substitui múltiplos valores em uma string por novos valores utilizando StringBuilder para maior performance.
        /// </summary>
        /// <param name="text">Texto de entrada.</param>
        /// <param name="replacements">Dicionário contendo os pares de substituição.</param>
        /// <returns>
        /// String com todos os valores substituídos.
        /// </returns>
        public static string ReplaceMultiple(string? text, Dictionary<string, string> replacements)
        {
            if (string.IsNullOrWhiteSpace(text) || replacements == null || replacements.Count == 0)
                return string.Empty;

            var sb = new StringBuilder(text);

            foreach (var pair in replacements)
            {
                sb.Replace(pair.Key, pair.Value);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Limita o tamanho de uma string e adiciona reticências (...) se exceder o tamanho máximo.
        /// </summary>
        /// <param name="text">Texto de entrada.</param>
        /// <param name="maxLength">Tamanho máximo permitido.</param>
        /// <returns>
        /// Uma string truncada com reticências se necessário.
        /// </returns>
        public static string LimitLength(string? text, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            if (text.Length <= maxLength)
                return text;

            return text.Substring(0, maxLength).Trim() + "...";
        }

    }
}
