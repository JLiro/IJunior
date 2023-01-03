using System;

namespace Task05SubarrayRepetitionsOfNumbers
{
    internal class Program
    {
        static void Main()
        {
            int[] array = new int[30];

            int minRandomValue = 10;
            int maxRandomValue = 20;
            Random random = new Random();

            int initialRepeatCount = 1;
            int currentRepeatCount = initialRepeatCount;

            int repeatNumber = 0;
            int maxRepeatCount = 0;

            for (int row = 0; row < array.GetLength(0); row++)
            {
                array[row] = random.Next(minRandomValue, maxRandomValue);

                Console.Write(array[row] + " ");
            }

            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] == array[i + 1])
                {
                    currentRepeatCount++;

                    if (currentRepeatCount > maxRepeatCount)
                    {
                        maxRepeatCount = currentRepeatCount;
                        repeatNumber = array[i];
                    }
                }
                else
                {
                    currentRepeatCount = initialRepeatCount;
                }
            }

            Console.WriteLine($"\nМаксимальная длина подмассива из одинаковых чисел: {maxRepeatCount}" +
                              $"\nЧисло, которое повторяется наибольшее количество раз: {repeatNumber}");
            Console.ReadKey();
        }
    }
}
