using Utilities.Validations;

namespace Utilities.Formatters;

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

    /// <summary>
    /// Remove a máscara do CNPJ, mantendo apenas os dígitos numéricos.
    /// </summary>
    /// <param name="cnpjWithMask">CNPJ formatado com máscara ou string contendo números.</param>
    /// <returns>
    /// String contendo apenas os dígitos do CNPJ, sem formatação.
    /// Retorna string vazia se o parâmetro for nulo, vazio ou contiver apenas espaços.
    /// </returns>
    /// <example>
    /// <code>
    /// string result1 = CnpjFormatter.RemoveMask("12.345.678/0001-95");    // "12345678000195"
    /// string result2 = CnpjFormatter.RemoveMask("abc12345678000195xyz");  // "12345678000195"
    /// string result3 = CnpjFormatter.RemoveMask("12345678000195");        // "12345678000195"
    /// string result4 = CnpjFormatter.RemoveMask("   ");                   // ""
    /// </code>
    /// </example>
    /// <remarks>
    /// Este método extrai apenas os dígitos numéricos da string, removendo todos os outros caracteres.
    /// Não valida se o CNPJ tem 14 dígitos ou se é um número válido.
    /// </remarks>
    public static string RemoveMask(string? cnpjWithMask)
    {
        if (string.IsNullOrWhiteSpace(cnpjWithMask))
            return string.Empty;

        return StringConversor.OnlyNumbers(cnpjWithMask);
    }
}
