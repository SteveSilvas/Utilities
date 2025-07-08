namespace Utilities.Generators
{
    /// <summary>
    /// Gerador de textos aleatórios
    /// </summary>
    public static class StringGenerator
    {
        private static readonly Random _random = new Random();

        /// <summary>
        /// Gera uma string aleatória apenas com letras (A-Z, a-z) de tamanho definido.
        /// </summary>
        /// <param name="limit">
        /// Quantidade de caracteres retornados pelo método.
        /// </param>
        /// <returns>
        /// Retorna um texto aleatório com tamanho definido pelo argumento limit.
        /// </returns>
        public static string GenerateRandomLetters(int limit = 1)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Range(0, limit)
                .Select(_ => letters[_random.Next(letters.Length)])
                .ToArray());
        }

        /// <summary>
        /// Gera uma string aleatória com letras e números (A-Z, a-z, 0-9) de tamanho definido.
        /// </summary>
        /// <param name="limit">
        /// Quantidade de caracteres retornados pelo método.
        /// </param>
        /// <returns>
        /// Retorna um texto aleatório com tamanho definido pelo argumento limit.
        /// </returns>
        public static string GenerateRandomAphaNumber(int limit = 1)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Range(0, limit)
                .Select(_ => chars[_random.Next(chars.Length)])
                .ToArray());
        }
    }
}
