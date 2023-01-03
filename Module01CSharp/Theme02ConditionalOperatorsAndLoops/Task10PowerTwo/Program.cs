using System;

namespace Task10PowerTwo
{
    internal class Program
    {
        static void Main()
        {
            Random random = new Random();
            int minRandomValue = 10;
            int maxRandomValue = 100;
            int randomNumber = random.Next(minRandomValue, maxRandomValue);
            int degreeNumber = 0;
            int numberPower = 1;
            int baseNumber = 2;

            Console.WriteLine($"Рандомное число: {randomNumber}");

            while (numberPower <= randomNumber)
            {
                numberPower *= baseNumber;
                degreeNumber++;
            }

            Console.WriteLine($"Минимальная степень двойки, превосходящая число {randomNumber}: {degreeNumber}");
            Console.ReadKey();
        }
    }
}
