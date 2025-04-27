namespace Utilities.Validations
{
    public static class StringConversor
    {
        public static string OnlyNumbers(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return new string(input.Where(char.IsDigit).ToArray());
        }
    }
}