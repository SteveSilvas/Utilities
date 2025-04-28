using System.Text.RegularExpressions;

namespace Utilities.Validations
{
    public static class EmailValidator
    {
        private const string EmailRegexPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$";

        public static bool IsValid(string? email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            if (email.Length > 253)
                return false;

            var parts = email.Split('@');

            if (parts.Length != 2)
                return false;

            var localPart = parts[0];

            if (localPart.Length > 64)
                return false;

            if (localPart.Contains(".."))
                return false;

            if (localPart.StartsWith(".") || localPart.EndsWith("."))
                return false;

            var domainPart = parts[1];
            var domainLabels = domainPart.Split('.');

            foreach (var label in domainLabels)
            {
                if (string.IsNullOrEmpty(label)) return false;
                if (label.StartsWith("-") || label.EndsWith("-"))
                    return false;

                if (label.Length > 63)
                    return false;

                if (label.Contains(".."))
                    return false;
            }

            return Regex.IsMatch(email, EmailRegexPattern, RegexOptions.IgnoreCase);
        }
    }
}
