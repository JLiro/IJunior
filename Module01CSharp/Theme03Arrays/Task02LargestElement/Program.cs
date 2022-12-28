using System;

namespace Task02LargestElement
{
    internal class Program
    {
        static void Main()
        {
            int[,] array = new int[10, 10];
            int maxElement = 0;

            Random random = new Random();

            for (int column = 0; column < array.GetLength(0); column++)
            {
                for (int row = 0; row < array.GetLength(1); row++)
                {
                    array[column, row] = random.Next(10, 100);

                    maxElement = maxElement < array[column, row] ? array[column, row] : maxElement;

                    Console.Write(array[column, row] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nМаксимальный элемент: " + maxElement);
            Console.ReadKey();
        }
    }
}
