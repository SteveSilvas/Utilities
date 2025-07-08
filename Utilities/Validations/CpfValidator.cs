namespace Utilities.Validations
{
    /// <summary>
    /// Fornece métodos estáticos para validação de números de CPF (Cadastro de Pessoas Físicas) brasileiros.
    /// </summary>
    /// <remarks>
    /// Esta classe implementa o algoritmo de validação de CPF, incluindo verificações de tamanho,
    /// dígitos repetidos e cálculo dos dígitos verificadores.
    /// </remarks>
    public static class CpfValidator
    {
        private const int TamanhoCpf = 11;
        private static readonly HashSet<string> _cpfsInvalidos = new HashSet<string>
        {
            "00000000000",
            "11111111111",
            "22222222222",
            "33333333333",
            "44444444444",
            "55555555555",
            "66666666666",
            "77777777777",
            "88888888888",
            "99999999999"
        };

        /// <summary>
        /// Valida se uma string fornecida é um número de CPF válido.
        /// </summary>
        /// <param name="numero">A string contendo o número do CPF (pode incluir formatação como pontos e hífen).</param>
        /// <returns>
        /// <c>true</c> se o CPF for válido; caso contrário, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// O método executa as seguintes etapas de validação:
        /// <list type="bullet">
        /// <item><description>Remove qualquer caractere não numérico do CPF.</description></item>
        /// <item><description>Verifica se o CPF resultante não é nulo, vazio ou tem um tamanho inválido (deve ter 11 dígitos).</description></item>
        /// <item><description>Verifica se o CPF consiste em dígitos repetidos (ex: "111.111.111-11").</description></item>
        /// <item><description>Calcula e verifica os dois dígitos verificadores.</description></item>
        /// </list>
        /// </remarks>
        public static bool IsCpf(string? numero)
        {
            string cpfTratado = StringConversor.OnlyNumbers(numero);
            if (string.IsNullOrEmpty(cpfTratado) || !PossuiTamanhoValido(cpfTratado)) return false;
            if (PossuiDigitosRepetidos(cpfTratado)) return false;
            return PossuiDigitosValidos(cpfTratado);
        }

        /// <summary>
        /// Verifica se o CPF possui o tamanho padrão de 11 dígitos.
        /// </summary>
        /// <param name="cpf">A string do CPF sem formatação.</param>
        /// <returns><c>true</c> se o CPF tiver 11 dígitos; caso contrário, <c>false</c>.</returns>
        private static bool PossuiTamanhoValido(string cpf) => cpf.Length == TamanhoCpf;

        /// <summary>
        /// Verifica se o CPF é uma sequência de dígitos repetidos (ex: "11111111111"), que são considerados inválidos.
        /// </summary>
        /// <param name="cpf">A string do CPF sem formatação.</param>
        /// <returns><c>true</c> se o CPF consistir em dígitos repetidos; caso contrário, <c>false</c>.</returns>
        private static bool PossuiDigitosRepetidos(string cpf) => _cpfsInvalidos.Contains(cpf);

        /// <summary>
        /// Realiza o cálculo e a verificação dos dois dígitos verificadores do CPF.
        /// </summary>
        /// <param name="cpf">A string do CPF sem formatação (11 dígitos).</param>
        /// <returns><c>true</c> se os dígitos verificadores calculados corresponderem aos presentes no CPF; caso contrário, <c>false</c>.</returns>
        private static bool PossuiDigitosValidos(string cpf)
        {
            var numero = cpf.Substring(0, TamanhoCpf - 2);

            var primeiroDigito = CalculaDigito(numero, new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 });
            var segundoDigito = CalculaDigito(numero + primeiroDigito, new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 });

            return string.Concat(primeiroDigito, segundoDigito) == cpf.Substring(TamanhoCpf - 2, 2);
        }

        /// <summary>
        /// Calcula um dígito verificador para uma dada parte do CPF e conjunto de multiplicadores.
        /// Este método implementa a lógica padrão de cálculo de DV para CPF.
        /// </summary>
        /// <param name="parteCpf">A parte do CPF a ser usada no cálculo (ex: os primeiros 9 dígitos, ou 9 dígitos + 1º DV).</param>
        /// <param name="multiplicadores">Um array de inteiros representando os multiplicadores a serem aplicados.</param>
        /// <returns>O dígito verificador calculado como uma string.</returns>
        private static string CalculaDigito(string parteCpf, int[] multiplicadores)
        {
            int soma = 0;
            for (int i = 0; i < multiplicadores.Length; i++)
            {
                soma += int.Parse(parteCpf[i].ToString()) * multiplicadores[i];
            }
            int resto = soma % 11;
            return (resto < 2 ? 0 : 11 - resto).ToString();
        }
    }
}