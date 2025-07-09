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
        /// Converte uma string para o formato camelCase com palavras juntas.
        /// A primeira letra de cada palavra (exceto a primeira palavra, se aplicável) será maiúscula,
        /// e a string resultante começará com letra minúscula.
        /// </summary>
        /// <param name="text">A string de entrada a ser convertida.</param>
        /// <returns>A string convertida para camelCase, ou uma string vazia se a entrada for nula, vazia ou consistir apenas em espaços em branco.</returns>
        public static string ToCamelCase(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0)
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < words.Length; i++)
            {
                string currentWord = words[i];

                if (i == 0)
                {
                    // A primeira palavra deve começar com letra minúscula
                    sb.Append(currentWord.ToLowerInvariant());
                }
                else
                {
                    sb.Append(char.ToUpperInvariant(currentWord[0]));
                    sb.Append(currentWord.Substring(1).ToLowerInvariant());

                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converte uma string para o formato PascalCase com palavras juntas iniciando cada palavra com letra maiúsucla.
        /// A primeira letra de cada palavra será maiúscula,
        /// e a string resultante começará com letra maiúscula.
        /// </summary>
        /// <param name="text">A string de entrada a ser convertida.</param>
        /// <returns>A string convertida para PascalCase, ou uma string vazia se a entrada for nula, vazia ou consistir apenas em espaços em branco.</returns>
        public static string ToPascalCase(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0)
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < words.Length; i++)
            {
                string currentWord = words[i];

                //if (i == 0)
                //{
                //    // A primeira palavra deve começar com letra maiúscula
                //    sb.Append(currentWord.ToUpperInvariant());
                //}
                //else
                //{
                sb.Append(char.ToUpperInvariant(currentWord[0]));
                sb.Append(currentWord.Substring(1).ToLowerInvariant());

                //}
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converte uma string para o formato SnakeCase com palavras separadas por underline em letras minúsculas.
        /// </summary>
        /// <param name="text">A string de entrada a ser convertida.</param>
        /// <returns>
        /// A string convertida para SnakeCase, ou uma string vazia se a entrada for nula, vazia ou consistir apenas em espaços em branco.
        /// </returns>
        public static string ToSnakeCase(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0)
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < words.Length; i++)
            {
                string currentWord = words[i];

                if (i > 0)
                {
                    sb.Append("_");
                }
                sb.Append(currentWord.ToLowerInvariant());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Converte uma string para o formato KebabCase com palavras separadas por hífem em letras minúsculas.
        /// </summary>
        /// <param name="text">A string de entrada a ser convertida.</param>
        /// <returns>
        /// A string convertida para KebabCase, ou uma string vazia se a entrada for nula, vazia ou consistir apenas em espaços em branco.
        /// </returns>
        public static string ToKebabCase(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0)
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < words.Length; i++)
            {
                string currentWord = words[i];

                if (i > 0)
                {
                    sb.Append("-");
                }
                sb.Append(currentWord.ToLowerInvariant());
            }
            return sb.ToString();
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

            return new string(text.Where(char.IsLetterOrDigit)?.ToArray());
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

        /// <summary>
        /// Remove todas as tags HTML de uma string, deixando apenas o texto puro.
        /// </summary>
        /// <param name="text">Texto de entrada que pode conter HTML.</param>
        /// <returns>
        /// Uma string limpa sem marcações HTML.
        /// </returns>
        public static string SanitizeHtml(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            // Decodifica entidades HTML (&lt;div&gt; → <div>)
            text = System.Net.WebUtility.HtmlDecode(text);

            // Remove <script>...</script> e <style>...</style>
            text = Regex.Replace(text, @"<script.*?>.*?</script>", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"<style.*?>.*?</style>", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            // Remove todas as outras tags HTML
            text = Regex.Replace(text, @"<[^>]+>", string.Empty, RegexOptions.Singleline);

            return text.Trim();
        }
    }
}
