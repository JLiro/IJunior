using System;

namespace Task03LocalMaxima
{
    internal class Program
    {
        static void Main()
        {
            Random random = new Random();
            int randomMinValue = 0;
            int randomMaxValue = 100;

            const int arraySize = 30;
            int[] array = new int[arraySize];

            Console.WriteLine("Массив: ");

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(randomMinValue, randomMaxValue + 1);
                Console.Write(array[i] + " ");
            }

            Console.Write("\n\nЛокальные максимумы:\n");

            if (array[0] > array[1])
            {
                Console.Write(array[0] + " ");
            }

            for (int i = 1; i < arraySize - 1; ++i)
            {
                if (array[i - 1] < array[i] && array[i + 1] < array[i])
                {
                    Console.Write(array[i] + " ");
                }
            }

            if (array[array.Length - 1] > array[array.Length - 2])
            {
                Console.Write(array[array.Length - 1] + " ");
            }

            Console.ReadLine();
        }
    }
}
