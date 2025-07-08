namespace Utilities.Validations
{
    /// <summary>
    /// Fornece métodos estáticos para validações comuns de strings.
    /// </summary>
    /// <remarks>
    /// Esta classe auxilia na verificação de propriedades específicas de strings, como a composição por dígitos.
    /// </remarks>
    public static class StringValidator
    {
        /// <summary>
        /// Verifica se a string de entrada consiste apenas em dígitos numéricos.
        /// </summary>
        /// <param name="input">A string a ser verificada.</param>
        /// <returns>
        /// <c>true</c> se a string for não nula, não vazia e contiver apenas dígitos;
        /// caso contrário, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// Um retorno de <c>false</c> ocorrerá se a string for nula, vazia ou contiver qualquer caractere que não seja um dígito (0-9).
        /// </remarks>
        public static bool IsDigit(string? input)
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