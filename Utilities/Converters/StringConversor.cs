using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilities.Validations
{
    public static class StringConversor
    {
        public static string OnlyNumbers(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return new string(input.Where(c => char.IsAsciiDigit(c)).ToArray());
        }

        public static string OnlyLetters(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return new string(input.Where(char.IsLetter).ToArray());
        }

        public static string Slugify(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            string result = input.ToLowerInvariant();

            result = result.Replace("ß", "ss"); // Alemão
            result = result.Replace("æ", "ae"); // Norueguês/Dinamarquês
            result = result.Replace("ø", "o");  // Norueguês/Dinamarquês
            result = result.Replace("å", "aa"); // Norueguês/Dinamarquês
            result = RemoveDiacritics(result);
            result = result.Replace("_", "-");

            result = Regex.Replace(result, @"[^a-z0-9\s-]", "");

            result = Regex.Replace(result, @"[\s-]+", "-").Trim('-');

            return result;
        }

        public static string RemoveDiacritics(string? text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var sb = new StringBuilder(text);

            sb.Replace("ß", "ss");
            sb.Replace("ẞ", "SS");
            sb.Replace("æ", "ae");
            sb.Replace("ø", "o");
            sb.Replace("å", "aa");

            string processedText = sb.ToString();

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