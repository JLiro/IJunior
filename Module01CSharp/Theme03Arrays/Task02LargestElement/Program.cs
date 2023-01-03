using System;

namespace Task02LargestElement
{
    internal class Program
    {
        static void Main()
        {
            Random random = new Random();
            int RandomMinValue = -99;
            int RandomMaxValue = -10;
            
            int rowArrayValue = 10;
            int colArrayValue = 10;
            int[,] array = new int[rowArrayValue, colArrayValue];
            int maxElement;
            int replacementValue = 0;

            for (int column = 0; column < array.GetLength(0); column++)
            {
                for (int row = 0; row < array.GetLength(1); row++)
                {
                    array[column, row] = random.Next(RandomMinValue, RandomMaxValue);

                    Console.Write(array[column, row] + " ");
                }

                Console.WriteLine();
            }

            maxElement = array[0, 0];

            for (int column = 0; column < array.GetLength(0); column++)
            {
                for (int row = 0; row < array.GetLength(1); row++)
                {
                    maxElement = maxElement < array[column, row] ? array[column, row] : maxElement;
                }
            }

            Console.WriteLine(
                             $"\nМаксимальный элемент: {maxElement}" +
                              "\n" +
                              "\nМассив, в котором он заменен нулем:"
                             );

            for (int column = 0; column < array.GetLength(0); column++)
            {
                for (int row = 0; row < array.GetLength(1); row++)
                {
                    array[column, row] = array[column, row] == maxElement ? replacementValue : array[column, row];

                    Console.Write(array[column, row] + " ");
                }

                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
