namespace Utilities.Validations
{
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

        public static bool IsCpf(string numero)
        {
            var cpfTratado = StringConversor.OnlyNumbers(numero);
            if (string.IsNullOrEmpty(cpfTratado) || !PossuiTamanhoValido(cpfTratado)) return false;
            if (PossuiDigitosRepetidos(cpfTratado)) return false;
            return PossuiDigitosValidos(cpfTratado);
        }

        private static bool PossuiTamanhoValido(string cpf) => cpf.Length == TamanhoCpf;

        private static bool PossuiDigitosRepetidos(string cpf) => _cpfsInvalidos.Contains(cpf);

        private static bool PossuiDigitosValidos(string cpf)
        {
            var numero = cpf.Substring(0, TamanhoCpf - 2);

            var primeiroDigito = CalculaDigito(numero, new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 });
            var segundoDigito = CalculaDigito(numero + primeiroDigito, new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 });

            return string.Concat(primeiroDigito, segundoDigito) == cpf.Substring(TamanhoCpf - 2, 2);
        }

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