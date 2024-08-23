using System;
using System.Linq;

namespace RevisaoRecursividade.Exercicios
{
    public class ContaOcorrencia
    {
        public static int ContarOcorrenciasRecursiva(int[] array, int numeroContado, int indice)
        {
            if (indice == array.Length) {
                return 0;
            }

            int contador = (array[indice] == numeroContado) ? 1 : 0;
            return contador + ContarOcorrenciasRecursiva(array, numeroContado, indice + 1);
        }

        public static int[] LerArray(string entrada)
        {
            string[] elementos = entrada.Split(';');
            int[] array = new int[elementos.Length];
            for (int i = 0; i < elementos.Length; i++) {
                array[i] = int.Parse(elementos[i].Trim());
            }
            return array;
        }

        public static void Main(string[] args)
        {
            while (true) {
                string entradaArray = Console.ReadLine();
                if (entradaArray.Equals("FIM", StringComparison.OrdinalIgnoreCase)) {
                    break;
                }

                int[] array = LerArray(entradaArray);
                int x = int.Parse(Console.ReadLine().Trim());
                int ocorrencias = ContarOcorrenciasRecursiva(array, x, 0);

                Console.WriteLine(ocorrencias);
            }
        }
    }

}