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
            int minNumber = 100;
            int maxNumber = 999;
            int quantityNumbers = 0;
            bool isQuantityNumbers;

            Console.WriteLine($"Рандомное число: {desiredNumber}");

            for (int i = desiredNumber; i <= maxNumber; i += desiredNumber)
            {
                isQuantityNumbers = i >= minNumber;
                if (isQuantityNumbers)
                {
                    quantityNumbers++;
                }
            }

            Console.WriteLine($"Количество трехзначных натуральных чисел, кратных {desiredNumber}: {quantityNumbers}");
            Console.ReadKey();
        }
    }
}
