using Utilities.Validations;

namespace Utilities.Formatters
{
    /// <summary>
    /// Adiciona a máscara padrão de CPF (xxx.xxx.xxx-xx) a uma string numérica.
    /// </summary>
    public static class CpfFormatter
    {
        /// <summary>
        /// Aplica a máscara de CPF (xxx.xxx.xxx-xx) a uma string contendo 11 dígitos.
        /// </summary>
        /// <remarks>
        /// Este método não valida o número do CPF; ele apenas aplica a máscara se houver 11 dígitos numéricos.
        /// Para validação do CPF, use <see cref="Utilities.Validations.CpfValidator"/>.
        /// <param name="cpf">String com os números do CPF (com ou sem pontuação).</param>
        /// </remarks>
        /// <returns>
        /// O CPF formatado, ou uma string vazia se o input for nulo ou inválido.
        /// </returns>

        public static string AddMask(string? cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return string.Empty;

            var digits = new string(cpf.Where(char.IsDigit).ToArray());

            if (digits.Length == 11)
                return Convert.ToUInt64(digits).ToString(@"000\.000\.000\-00");

            return string.Empty;
        }

        /// <summary>
        /// Remove a máscara do CPF, mantendo apenas os dígitos numéricos.
        /// </summary>
        /// <param name="cpfWithMask">CPF formatado com máscara ou string contendo números.</param>
        /// <returns>
        /// String contendo apenas os dígitos do CNPJ, sem formatação.
        /// Retorna string vazia se o parâmetro for nulo, vazio ou contiver apenas espaços.
        /// </returns>
        /// <example>
        /// <code>
        /// string result1 = CpfFormatter.RemoveMask("123.456.789-11");    // "12345678911"
        /// string result2 = CpfFormatter.RemoveMask("abc12345678911xyz");  // "12345678911"
        /// string result3 = CpfFormatter.RemoveMask("12345678000195");        // "12345678000195"
        /// string result4 = CpfFormatter.RemoveMask("   ");                   // ""
        /// </code>
        /// </example>
        /// <remarks>
        /// Este método extrai apenas os dígitos numéricos da string, removendo todos os outros caracteres.
        /// Não valida se o CPF tem 11 dígitos ou se é um número válido.
        /// </remarks>
        public static string RemoveMask(string? cpfWithMask)
        {
            if (string.IsNullOrWhiteSpace(cpfWithMask))
                return string.Empty;

            return StringConversor.OnlyNumbers(cpfWithMask);
        }
    }
}
