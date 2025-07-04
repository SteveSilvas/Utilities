namespace Utilities.Formatters
{
    public static class CnpjFormatter
    {
        public static string AddMask(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var digits = new string(input.Where(char.IsDigit).ToArray());

            if (digits.Length == 14)
                return Convert.ToUInt64(digits).ToString(@"00\.000\.000\/0000\-00");

            return input;
        }
    }
}
