using System;

namespace Task05SubarrayRepetitionsOfNumbers
{
    internal class Program
    {
        static void Main()
        {
            int[] array = new int[30] { 3, 3, 3, 4, 5, 6, 7, 2, 2, 3, 2, 5, 7, 5, 7, 7, 8, 8, 8, 8, 4, 5, 8, 1, 2, 4, 5, 6, 8, 5 };
            int number = 0;
            int quantityRepeatOfNumber = 1;
            int maxLengthRepeatNumbers = 1;

            Console.Write(array[0] + " ");

            for (int i = 1; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");

                if (array[i] == array[i - 1])
                {
                    quantityRepeatOfNumber++;
                }

                if (quantityRepeatOfNumber > maxLengthRepeatNumbers)
                {
                    maxLengthRepeatNumbers = quantityRepeatOfNumber;
                    number = array[i];
                }
            }

            Console.WriteLine($"\nМаксимальная длина подмассива из одинаковых чисел: {maxLengthRepeatNumbers}");
            Console.WriteLine($"Число, которое повторяется самое большое количество раз: {number}");
            Console.ReadKey();
        }
    }
}
