namespace Utilities.Formatters
{
    /// <summary>
    /// Fornece métodos para formatação de CNPJs.
    /// </summary>
    public static class CnpjFormatter
    {
        /// <summary>
        /// Aplica a máscara padrão de CNPJ (<c>00.000.000/0000-00</c>) a uma string com 14 dígitos.
        /// </summary>
        /// <param name="input">CNPJ sem máscara ou já formatado.</param>
        /// <returns>
        /// CNPJ formatado se tamanho válido; caso contrário, uma string vazia.
        /// </returns>
        /// <remarks>
        /// Este método não valida o número do CNPJ; ele apenas aplica a máscara se houver 14 dígitos numéricos.
        /// Para validação do CNPJ, use <see cref="Utilities.Validations.CnpjValidator"/>.
        /// </remarks>
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
