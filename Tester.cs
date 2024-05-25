using System;
using System.Collections.Generic;

namespace Vector
{
    // Comparer for ascending order
    public class AscendingIntComparer : IComparer<int>
    {
        public int Compare(int A, int B)
        {
            return A - B;
        }
    }

    // Comparer for descending order
    public class DescendingIntComparer : IComparer<int>
    {
        public int Compare(int A, int B)
        {
            return B - A;
        }
    }

    // Comparer to sort even numbers before odd numbers
    public class EvenNumberFirstComparer : IComparer<int>
    {
        public int Compare(int A, int B)
        {
            return A % 2 - B % 2;
        }
    }

    // Student class for demonstrating object sorting
    public class Student : IComparable<Student>
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public int CompareTo(Student s)
        {
            return this.Id - s.Id;
        }

        public override string ToString()
        {
            return Id + "[" + Name + "]";
        }
    }

    class Tester
    {
        private static int lcsTestsPassed = 0;
        private static int lcsTestsFailed = 0;
        private static int sortingTestsPassed = 0;
        private static int sortingTestsFailed = 0;

        // Check if array is sorted in ascending order
        private static bool CheckAscending(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
                if (array[i] > array[i + 1]) return false;
            return true;
        }

        // Check if array is sorted in descending order
        private static bool CheckDescending(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
                if (array[i] < array[i + 1]) return false;
            return true;
        }

        // Check if array is sorted with even numbers first
        private static bool CheckEvenNumberFirst(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
                if (array[i] % 2 > array[i + 1] % 2) return false;
            return true;
        }

        // Check if array matches a certificate array
        private static bool CheckSequence<T>(T[] certificate, T[] array) where T : IComparable<T>
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (certificate.Length != array.Length) return false;
            for (int i = 0; i < certificate.Length; i++)
            {
                if (!certificate[i].Equals(array[i])) return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            // Run LCS Tests
            RunLCSTests();

            // Run Sorting Tests
            RunSortingTests();

            // Print Test Summary
            PrintTestSummary();

            Console.WriteLine("\nAll tests completed.");
        }

        // Run and log LCS tests
        static void RunLCSTests()
        {
            Console.WriteLine("Running LCS Tests:\n");
            TestLCS("AGGTAB", "GXTXAYB", "GTAB");
            TestLCS("ABCBDAB", "BDCAB", "BCAB");
            TestLCS("XMJYAUZ", "MZJAWXU", "MJAU");
            TestLCS("", "ABC", "");
            TestLCS("ABC", "", "");
            TestLCS("ABC", "DEF", "");
        }

        // Test a specific LCS case
        private static void TestLCS(string X, string Y, string expectedLCS)
        {
            int[,] L = LCS.ComputeLCSMatrix(X, Y);
            Console.WriteLine($"LCS matrix for X: {X}, Y: {Y}");
            LCS.PrintLCSMatrix(L, X, Y);

            string lcs = LCS.GetLCS(X, Y, L);
            Console.WriteLine($"LCS of {X} and {Y} is {lcs}");
            Console.WriteLine($"Expected LCS: {expectedLCS}");

            if (lcs == expectedLCS)
            {
                Console.WriteLine("Test Passed");
                lcsTestsPassed++;
            }
            else
            {
                Console.WriteLine("Test Failed");
                lcsTestsFailed++;
            }
            Console.WriteLine();
        }

        // Run and log sorting tests
        static void RunSortingTests()
        {
            Console.WriteLine("\nRunning Sorting Tests:");
            int[] data = { 5, 3, 8, 6, 2, 7, 4, 1 };

            // Ascending Order Test
            TestSorting(data, new AscendingIntComparer(), CheckAscending);

            // Descending Order Test
            TestSorting(data, new DescendingIntComparer(), CheckDescending);

            // Even Number First Test
            TestSorting(data, new EvenNumberFirstComparer(), CheckEvenNumberFirst);
        }

        // Test a specific sorting case
        private static void TestSorting(int[] data, IComparer<int> comparer, Func<int[], bool> checkOrder)
        {
            ISorter sorter = new ArraySorter();
            int[] array = (int[])data.Clone();
            sorter.Sort(array, comparer);

            Console.WriteLine("Sorted array: " + string.Join(", ", array));

            if (checkOrder(array))
            {
                Console.WriteLine("Test Passed");
                sortingTestsPassed++;
            }
            else
            {
                Console.WriteLine("Test Failed");
                sortingTestsFailed++;
            }
            Console.WriteLine();
        }

        // Print summary of all tests
        private static void PrintTestSummary()
        {
            Console.WriteLine("Test Summary:\n");
            Console.WriteLine($"LCS Tests Passed: {lcsTestsPassed}");
            Console.WriteLine($"LCS Tests Failed: {lcsTestsFailed}\n");
            Console.WriteLine($"Sorting Tests Passed: {sortingTestsPassed}");
            Console.WriteLine($"Sorting Tests Failed: {sortingTestsFailed}");
        }
    }
}
