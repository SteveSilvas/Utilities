namespace Utilities.Validations
{
    /// <summary>
    /// Fornece funcionalidade para calcular um dígito verificador com base em um conjunto configurável de regras.
    /// </summary>
    /// <remarks>A classe <see cref="DigitoVerificador"/> permite a customização do processo de cálculo,
    /// incluindo o uso de multiplicadores específicos, aritmética modular, substituição de dígitos e outras opções.
    /// É comumente usada em cenários onde um checksum ou dígito verificador é requerido, como na validação
    /// de números de identificação ou códigos.</remarks>
    public class DigitoVerificador
    {
        private string _numero;
        private int _modulo = 11;
        private int _valorLimite = 0;
        private int _valorSubstituto = 0;
        private bool _limite = false;
        private bool _somarAlgarismos;
        private bool _complementarDoModulo = true;
        private readonly List<int> _multiplicadores = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IDictionary<int, string> _substituicoes = new Dictionary<int, string>();

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="DigitoVerificador"/> com o número base.
        /// </summary>
        /// <param name="numero">A string numérica para a qual o dígito verificador será calculado.</param>
        public DigitoVerificador(string numero)
        {
            _numero = numero;
        }

        /// <summary>
        /// Define uma sequência de multiplicadores que variam de um valor inicial até um valor final.
        /// </summary>
        /// <param name="primeiroMultiplicador">O primeiro multiplicador na sequência.</param>
        /// <param name="ultimoMultiplicador">O último multiplicador na sequência.</param>
        /// <returns>A própria instância de <see cref="DigitoVerificador"/> para permitir encadeamento de chamadas.</returns>
        public DigitoVerificador ComMultiplicadoresDeAte(int primeiroMultiplicador, int ultimoMultiplicador)
        {
            _multiplicadores.Clear();

            for (var i = primeiroMultiplicador; i <= ultimoMultiplicador; i++)
            {
                _multiplicadores.Add(i);
            }

            return this;
        }

        /// <summary>
        /// Define uma lista explícita de multiplicadores a serem utilizados no cálculo.
        /// </summary>
        /// <param name="multiplicadores">Um array de inteiros representando os multiplicadores.</param>
        /// <returns>A própria instância de <see cref="DigitoVerificador"/> para permitir encadeamento de chamadas.</returns>
        public DigitoVerificador ComMultiplicadores(params int[] multiplicadores)
        {
            _multiplicadores.Clear();

            foreach (var i in multiplicadores)
            {
                _multiplicadores.Add(i);
            }

            return this;
        }

        /// <summary>
        /// Configura substituições para dígitos verificadores específicos.
        /// </summary>
        /// <param name="substituto">A string que substituirá os dígitos especificados.</param>
        /// <param name="digitos">Um array de inteiros representando os dígitos a serem substituídos.</param>
        /// <returns>A própria instância de <see cref="DigitoVerificador"/> para permitir encadeamento de chamadas.</returns>
        public DigitoVerificador Substituindo(string substituto, params int[] digitos)
        {
            foreach (var i in digitos)
            {
                _substituicoes[i] = substituto;
            }

            return this;
        }

        /// <summary>
        /// Define o módulo a ser utilizado na operação de resto da divisão.
        /// </summary>
        /// <param name="modulo">O valor do módulo.</param>
        /// <returns>A própria instância de <see cref="DigitoVerificador"/> para permitir encadeamento de chamadas.</returns>
        public DigitoVerificador Modulo(int modulo)
        {
            _modulo = modulo;
            return this;
        }

        /// <summary>
        /// Habilita a soma dos algarismos do produto (dígito * multiplicador) antes de somar ao total.
        /// </summary>
        /// <returns>A própria instância de <see cref="DigitoVerificador"/> para permitir encadeamento de chamadas.</returns>
        public DigitoVerificador SomandoAlgarismos()
        {
            _somarAlgarismos = true;
            return this;
        }

        /// <summary>
        /// Inverte a ordem dos multiplicadores para o cálculo do dígito.
        /// </summary>
        /// <returns>A própria instância de <see cref="DigitoVerificador"/> para permitir encadeamento de chamadas.</returns>
        public DigitoVerificador InvertendoMultiplicadores()
        {
            _multiplicadores.Reverse();
            return this;
        }

        /// <summary>
        /// Desabilita o cálculo do complemento do módulo (o resultado será o próprio resto da divisão).
        /// </summary>
        /// <returns>A própria instância de <see cref="DigitoVerificador"/> para permitir encadeamento de chamadas.</returns>
        public DigitoVerificador SemComplementarDoModulo()
        {
            _complementarDoModulo = false;
            return this;
        }

        /// <summary>
        /// Inverte a ordem dos dígitos do número base antes do cálculo.
        /// </summary>
        /// <returns>A própria instância de <see cref="DigitoVerificador"/> para permitir encadeamento de chamadas.</returns>
        public DigitoVerificador InvertendoNumero()
        {
            _numero = new string(_numero.Reverse().ToArray());
            return this;
        }

        /// <summary>
        /// Habilita um limite para o resultado do módulo, substituindo o dígito por um valor específico se o limite for atingido.
        /// </summary>
        /// <param name="valorLimite">O valor limite (o dígito será substituído se o resultado do módulo for igual ou maior que este valor).</param>
        /// <param name="valorSubstituto">O valor pelo qual o dígito será substituído caso o limite seja atingido. O padrão é 0.</param>
        /// <returns>A própria instância de <see cref="DigitoVerificador"/> para permitir encadeamento de chamadas.</returns>
        public DigitoVerificador HabilitaLimiteModulo(int valorLimite, int valorSubstituto = 0)
        {
            _valorLimite = valorLimite;
            _valorSubstituto = valorSubstituto;
            _limite = true;
            return this;
        }

        /// <summary>
        /// Desabilita a regra de limite para o resultado do módulo.
        /// </summary>
        /// <returns>A própria instância de <see cref="DigitoVerificador"/> para permitir encadeamento de chamadas.</returns>
        public DigitoVerificador DesabilitaLimiteModulo()
        {
            _limite = false;
            _valorLimite = 0;
            _valorSubstituto = 0;
            return this;
        }

        /// <summary>
        /// Adiciona um dígito ao final do número base atual.
        /// </summary>
        /// <param name="digito">A string contendo o dígito a ser adicionado.</param>
        public void AddDigito(string digito) => _numero = string.Concat(_numero, digito);

        /// <summary>
        /// Calcula o dígito verificador com base nas configurações e no número base.
        /// </summary>
        /// <returns>O dígito verificador calculado como uma string. Retorna <see cref="string.Empty"/> se o número base for vazio.</returns>
        public string CalculaDigito() => _numero.Length > 0 ? ObterSomaDosDigitos() : string.Empty;

        /// <summary>
        /// Realiza a soma ponderada dos dígitos do número base de acordo com os multiplicadores e regras configuradas.
        /// </summary>
        /// <returns>O resultado final do cálculo do dígito verificador como uma string.</returns>
        private string ObterSomaDosDigitos()
        {
            var soma = 0;
            // Itera sobre o número de trás para frente, aplicando os multiplicadores
            for (int i = _numero.Length - 1, m = 0; i >= 0; i--)
            {
                var produto = (int)char.GetNumericValue(_numero[i]) * _multiplicadores[m];
                // Soma o produto ou a soma dos algarismos do produto
                soma += _somarAlgarismos ? SomaAlgarismos(produto) : produto;

                // Avança para o próximo multiplicador, resetando se atingir o final da lista
                if (++m >= _multiplicadores.Count) m = 0;
            }

            var mod = soma % _modulo; // Calcula o resto da divisão
            // Determina o resultado final com base no complemento do módulo
            var resultado = _complementarDoModulo ? _modulo - mod : mod;
            // Aplica a regra de limite se habilitada
            resultado = _limite && mod >= _valorLimite ? _valorSubstituto : resultado;

            // Retorna o resultado ou um substituto configurado
            return _substituicoes.ContainsKey(resultado) ? _substituicoes[resultado] : resultado.ToString();
        }

        /// <summary>
        /// Calcula a soma dos algarismos de um número de dois dígitos.
        /// Ex: SomaAlgarismos(15) = 1 + 5 = 6. SomaAlgarismos(7) = 0 + 7 = 7.
        /// </summary>
        /// <param name="produto">O número cujo algarismos serão somados.</param>
        /// <returns>A soma dos algarismos do número.</returns>
        private static int SomaAlgarismos(int produto) => (produto / 10) + (produto % 10);
    }
}