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
    }
}
