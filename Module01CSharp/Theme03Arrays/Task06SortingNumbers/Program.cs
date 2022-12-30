using System;

namespace Task06SortingNumbers
{
    internal class Program
    {
        static void Main()
        {
            int[] array = new int[15] { 4, 3, 5, 8, 1, 9, 5, 6, 5, 7, 2, 3, 7, 1, 8 };
            int tempNumber;

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] > array[j])
                    {
                        tempNumber = array[j];
                        array[j] = array[i];
                        array[i] = tempNumber;
                    }
                }
            }

            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }

            Console.ReadKey();
        }
    }
}
