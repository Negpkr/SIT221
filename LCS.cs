//*****
//This File is a TODO file for students
//*****
using System;
using System.Text;

namespace Vector
{
    public class LCS
    {
        // Method to compute the LCS matrix
        public static int[,] ComputeLCSMatrix(string X, string Y)
        {
            int m = X.Length;
            int n = Y.Length;
            int[,] L = new int[m + 1, n + 1];

            // Fill the LCS matrix using dynamic programming
            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        L[i, j] = 0;
                    }
                    else if (X[i - 1] == Y[j - 1])
                    {
                        L[i, j] = L[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        L[i, j] = Math.Max(L[i - 1, j], L[i, j - 1]);
                    }
                }
            }

            return L;
        }

        // Method to backtrack and find the LCS string
        public static string GetLCS(string X, string Y, int[,] L)
        {
            int i = X.Length;
            int j = Y.Length;
            StringBuilder lcs = new StringBuilder();

            // Backtrack from the bottom-right of the matrix
            while (i > 0 && j > 0)
            {
                if (X[i - 1] == Y[j - 1])
                {
                    lcs.Append(X[i - 1]);
                    i--;
                    j--;
                }
                else if (L[i - 1, j] >= L[i, j - 1])
                {
                    i--;
                }
                else
                {
                    j--;
                }
            }

            // Reverse the string since we built it backwards
            char[] lcsArray = lcs.ToString().ToCharArray();
            Array.Reverse(lcsArray);
            return new string(lcsArray);
        }

        // Method to print the LCS matrix
        public static void PrintLCSMatrix(int[,] L, string X, string Y)
        {
            int m = X.Length;
            int n = Y.Length;

            // Print the header row
            Console.Write("  ");
            for (int j = 0; j <= n; j++)
            {
                Console.Write(j == 0 ? "  " : Y[j - 1] + " ");
            }
            Console.WriteLine();

            // Print the matrix
            for (int i = 0; i <= m; i++)
            {
                Console.Write(i == 0 ? "  " : X[i - 1] + " ");
                for (int j = 0; j <= n; j++)
                {
                    Console.Write(L[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
