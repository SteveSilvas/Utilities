using Utilities.Validations;

namespace Utilities.Formatters
{
    /// <summary>
    /// Classe estática responsável por aplicar máscaras de formatação em números de telefone
    /// </summary>
    public static class PhoneFormatter
    {
        /// <summary>
        /// Aplica a máscara apropriada ao número de telefone baseado na quantidade de dígitos
        /// </summary>
        /// <param name="phoneNumber">Número de telefone a ser formatado</param>
        /// <returns>
        /// Retorna o número formatado com a máscara apropriada:
        /// - 8 dígitos: tttt-tttt
        /// - 10 dígitos: (dd) tttt-tttt  
        /// - 11 dígitos: (dd) t tttt-tttt
        /// Retorna string vazia se o número não atender aos critérios
        /// </returns>
        /// <example>
        /// <code>
        /// string result1 = PhoneFormatter.AddMask("12345678");     // "1234-5678"
        /// string result2 = PhoneFormatter.AddMask("1133334444");   // "(11) 3333-4444"
        /// string result3 = PhoneFormatter.AddMask("11933334444");  // "(11) 9 3333-4444"
        /// </code>
        /// </example>
        public static string AddMask(string? phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return string.Empty;

            string phoneCleaned = StringConversor.OnlyNumbers(phoneNumber);

            return phoneCleaned.Length switch
            {
                8 => FormatEightDigits(phoneCleaned),
                10 => FormatTenDigits(phoneCleaned),
                11 => FormatElevenDigits(phoneCleaned),
                _ => string.Empty
            };
        }

        /// <summary>
        /// Formata número de telefone com 8 dígitos no padrão tttt-tttt
        /// </summary>
        /// <param name="phoneNumber">Número limpo com 8 dígitos</param>
        /// <returns>Número formatado no padrão tttt-tttt</returns>
        private static string FormatEightDigits(string phoneNumber)
        {
            return $"{phoneNumber.Substring(0, 4)}-{phoneNumber.Substring(4, 4)}";
        }

        /// <summary>
        /// Formata número de telefone com 10 dígitos no padrão (dd) tttt-tttt
        /// </summary>
        /// <param name="phoneNumber">Número limpo com 10 dígitos</param>
        /// <returns>Número formatado no padrão (dd) tttt-tttt</returns>
        private static string FormatTenDigits(string phoneNumber)
        {
            return $"({phoneNumber.Substring(0, 2)}) {phoneNumber.Substring(2, 4)}-{phoneNumber.Substring(6, 4)}";
        }

        /// <summary>
        /// Formata número de celular com 11 dígitos no padrão (dd) t tttt-tttt
        /// </summary>
        /// <param name="phoneNumber">Número limpo com 11 dígitos</param>
        /// <returns>Número formatado no padrão (dd) t tttt-tttt</returns>
        private static string FormatElevenDigits(string phoneNumber)
        {
            return $"({phoneNumber.Substring(0, 2)}) {phoneNumber.Substring(2, 1)} {phoneNumber.Substring(3, 4)}-{phoneNumber.Substring(7, 4)}";
        }

        /// <summary>
        /// Remove a máscara do número de telefone, mantendo apenas os dígitos
        /// </summary>
        /// <param name="phoneNumber">Número de telefone formatado</param>
        /// <returns>Número de telefone sem formatação, contendo apenas dígitos</returns>
        /// <example>
        /// <code>
        /// string result = PhoneFormatter.RemoveMask("(11) 9 3333-4444"); // "11933334444"
        /// </code>
        /// </example>
        public static string RemoveMask(string? phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return string.Empty;

            return StringConversor.OnlyNumbers(phoneNumber).Replace(" ", "");
        }
    }
}