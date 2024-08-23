using System;
using System.Collections.Generic;
using System.Text;

namespace RevisaoRecursividade.Exercicios
{
    public class AlgebraBooleana
    {
        static void Main(string[] args)
        {
            string entrada;
            while ((entrada = Console.ReadLine()) != "0")
            {
                Console.WriteLine(Avaliar(entrada) ? 1 : 0);
            }
        }

        public static bool Avaliar(string entrada)
        {
            string[] espacoDividido = entrada.ToLower().Split(' ');
            int quantidadeVariaveis = int.Parse(espacoDividido[0]);
            bool[] valores = new bool[quantidadeVariaveis];

            for (int i = 0; i < valores.Length; i++) {
                int valor = int.Parse(espacoDividido[i + 1]);
                valores[i] = valor == 1;
            }

            string expressao = string.Join("", espacoDividido, valores.Length + 1,
                espacoDividido.Length - valores.Length - 1);

            return AvaliarRecursivamente(expressao, valores);
        }

        public static bool AvaliarRecursivamente(string expressao, bool[] variaveis)
        {
            if (expressao.Length == 1) {
                return variaveis[expressao[0] - 'a'];
            }

            int indiceInicio = expressao.IndexOf('(');
            int indiceFim = expressao.LastIndexOf(')');

            string funcao = expressao.Substring(0, indiceInicio);
            string parametrosFuncao = expressao.Substring(indiceInicio + 1, indiceFim - indiceInicio - 1);
            List<string> parametros = ExtrairParametros(parametrosFuncao);

            if (funcao == "not") {
                if (parametros.Count != 1) {
                    throw new ExcecaoSintaxe("Você deve fornecer pelo menos um parâmetro para a função not().");
                }

                return !AvaliarRecursivamente(parametros[0], variaveis);
            }
            else
            {
                if (parametros.Count < 2) {
                    throw new ExcecaoSintaxe(
                        $"Você deve fornecer pelo menos dois parâmetros para a função {funcao}().");
                }

                if (funcao == "and") {
                    foreach (string parametro in parametros)
                    {
                        if (!AvaliarRecursivamente(parametro, variaveis)) {
                            return false;
                        }
                    }

                    return true;
                } else if (funcao == "or") {
                    foreach (string parametro in parametros)
                    {
                        if (AvaliarRecursivamente(parametro, variaveis)) {
                            return true;
                        }
                    }
                    return false;
                } else {
                    throw new ExcecaoSintaxe($"A função {funcao}() não existe.");
                }
            }
        }

        private static List<string> ExtrairParametros(string expressao)
        {
            List<string> parametros = new List<string>();
            StringBuilder parametro = new StringBuilder();

            int profundidade = 0;

            foreach (char c in expressao)
            {
                if (c == ',' && profundidade == 0) {
                    parametros.Add(parametro.ToString());
                    parametro.Clear();
                } else {
                    parametro.Append(c);
                    if (c == '(') {
                        profundidade++;
                    } else if (c == ')') {
                        profundidade--;
                    }
                }
            }

            parametros.Add(parametro.ToString());
            return parametros;
        }

        public class ExcecaoSintaxe : Exception
        {
            public ExcecaoSintaxe(string mensagem) : base(mensagem)
            {
            }
        }
    }
}