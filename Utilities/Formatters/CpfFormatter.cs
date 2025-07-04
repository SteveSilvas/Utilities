namespace Utilities.Formatters
{
    public static class CpfFormatter
    {
        public static string AddMask(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var digits = new string(input.Where(char.IsDigit).ToArray());

            if (digits.Length == 11)
                return Convert.ToUInt64(digits).ToString(@"000\.000\.000\-00");

            return string.Empty;
        }
    }
}
