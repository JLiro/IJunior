using System;

namespace Task06SortingNumbers
{
    internal class Program
    {
        static void Main()
        {
            Random random = new Random();
            int randomMin = 10;
            int randomMax = 100;

            int arraySize = 10;
            int[] array = new int[arraySize];

            Console.WriteLine("Исходный массив:");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(randomMin, randomMax);
                Console.Write(array[i] + " ");
            }

            Console.WriteLine("\n\nОтсортированный массив:");

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] > array[j])
                    {
                        (array[i], array[j]) = (array[j], array[i]);
                    }
                }

                Console.Write(array[i] + " ");
            }

            Console.ReadLine();
        }
    }
}
