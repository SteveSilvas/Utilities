namespace Utilities.Formatters
{
    public class StringFormatter
    {
        public static string CleanSpaces(string? text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            return text.Trim();
        }
    }
}
