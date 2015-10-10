using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static List<int> endResults = new List<int>();
        static void Main(string[] args)
        {
            const string T = "aaaaaaaabbabbabbaa";
            const string P = "abba";

            string Sigma = ExtraxtAlphabet(T);

            Console.WriteLine("Zapytanie o tresci \"" + P + "\" jest na :");

            FiniteAutomationMatcher(T, ComputeTransactionFunction(P, Sigma), P.Length - 1, Sigma);
        }

        private static string ExtraxtAlphabet(string T)
        {
            string alphabet = "";
            foreach (char i in T)
            {
                if (!alphabet.Contains(i))
                {
                    alphabet = alphabet + i;
                }
            }
            return alphabet;
        }

        private static void FiniteAutomationMatcher(string T, int[][] delta, int m, string Sigma)
        {
            int number = 0;
            int q = 0;
            for (int i = 0; i < T.Length - 1; i++)
            {
                int temp = Sigma.IndexOf(T[i]);
                q = delta[q][temp];
                if (q == m)
                {
                    number++;
                    int results = (i - m) + 1;
                    Console.WriteLine("#" + number + ": " + results);
                    i = i - m;
                    endResults.Add(results);
                }
            }

            Console.Write("\nW tekscie: ");
            for (int i = 0; i < T.Count(); i++)
            {
                if (endResults.Contains(i))
                {
                    Console.Write(">");
                }
                Console.Write(T[i]);
            }
            Console.WriteLine("\nUdalo sie znalzc je " + number + " razy.");
        }

        private static int[][] ComputeTransactionFunction(string P, string Sigma)
        {
            int range = P.Length;
            int[][] delta = new int[range][];
            for (int i = 0; i < range; i++)
            {
                delta[i] = new int[Sigma.Length];
            }

            int k = 0;
            int m = range;
            for (int q = 0; q < m - 1; q++)
            {
                foreach (char a in Sigma)
                {
                    k = Math.Min(m + 1, q + 2);
                    string temp = P.Substring(0, q) + a;

                    do
                    {
                        k--;
                    }
                    while (!temp.EndsWith(P.Substring(0, k)));

                    delta[q][Sigma.IndexOf(a)] = k;
                }
            }
            return delta;
        }
    }
}
