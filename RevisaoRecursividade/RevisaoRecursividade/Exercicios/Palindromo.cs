using System;

namespace RevisaoRecursividade.Exercicios
{
    public class Palindromo
    {
        private static bool VerificarPalindromoRecursivo(string entrada)
        {
            if (entrada.Length <= 1) {
                return true;
            }

            if (entrada[0] != entrada[entrada.Length - 1]) {
                return false;
            }

            return VerificarPalindromoRecursivo(entrada.Substring(1, entrada.Length - 2));
        }

        public static void Main(string[] args)
        {
            while (true) {
                string entrada = Console.ReadLine();
                if (entrada.Equals("FIM")) {
                    break;
                }

                if (VerificarPalindromoRecursivo(entrada)) {
                    Console.WriteLine("SIM");
                } else {
                    Console.WriteLine("NAO");
                }
            }
        }
    }

}