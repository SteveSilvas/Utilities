namespace Utilities.Validations
{
    public static class StringValidator
    {
        public static bool IsDigit(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }
    }
}
