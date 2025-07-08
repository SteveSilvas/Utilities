using System.Text.RegularExpressions;

namespace Utilities.Validations
{
    /// <summary>
    /// Fornece métodos estáticos para validação de endereços de e-mail.
    /// </summary>
    /// <remarks>
    /// Esta classe implementa uma validação robusta de e-mail que verifica o formato geral do endereço,
    /// restrições de comprimento para partes locais e de domínio, e a conformidade com padrões comuns
    /// para endereços de e-mail. Ela combina verificações de regra de negócio com uma expressão regular.
    /// </remarks>
    public static class EmailValidator
    {
        /// <summary>
        /// Padrão de expressão regular para validação de e-mails, abrangendo a maioria dos caracteres permitidos.
        /// </summary>
        private const string EmailRegexPattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,}$";

        /// <summary>
        /// Valida se uma string fornecida é um endereço de e-mail válido.
        /// </summary>
        /// <param name="email">A string do endereço de e-mail a ser validada.</param>
        /// <returns>
        /// <c>true</c> se o e-mail for válido de acordo com as regras internas e o padrão de regex;
        /// caso contrário, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// O método verifica as seguintes condições:
        /// <list type="bullet">
        /// <item><description>Não nulo ou vazio.</description></item>
        /// <item><description>Comprimento total não excede 253 caracteres.</description></item>
        /// <item><description>Contém exatamente um símbolo '@'.</description></item>
        /// <item><description>Parte local (antes do '@') não excede 64 caracteres.</description></item>
        /// <item><description>Parte local não contém ".." (dois pontos consecutivos).</description></item>
        /// <item><description>Parte local não começa nem termina com ponto ('.').</description></item>
        /// <item><description>Rótulos de domínio (partes separadas por ponto no domínio) não são vazios.</description></item>
        /// <item><description>Rótulos de domínio não começam nem terminam com hífen ('-').</description></item>
        /// <item><description>Rótulos de domínio não excedem 63 caracteres.</description></item>
        /// <item><description>Rótulos de domínio não contêm ".." (dois pontos consecutivos).</description></item>
        /// <item><description>O e-mail corresponde ao padrão da expressão regular definida.</description></item>
        /// </list>
        /// </remarks>
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