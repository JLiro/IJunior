using System;

namespace Task09Multiples
{
    internal class Program
    {
        static void Main()
        {
            Random random = new Random();
            int minValueForRandom = 1;
            int maxValueForRandom = 28;
            int desiredNumber = random.Next(minValueForRandom, maxValueForRandom);
            int minTripleDigits = 100;
            int maxTripleDigits = 999;
            int quantityNumbers = 0;

            Console.WriteLine($"Рандомное число: {desiredNumber}");

            for (int i = desiredNumber; i <= maxTripleDigits; i += desiredNumber)
            {
                if (i >= minTripleDigits)
                {
                    quantityNumbers++;
                }
            }

            Console.WriteLine($"Количество трехзначных натуральных чисел, кратных {desiredNumber}: {quantityNumbers}");
            Console.ReadKey();
        }
    }
}
