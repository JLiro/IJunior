using System;

namespace Task10PowerTwo
{
    internal class Program
    {
        static void Main()
        {
            Random random = new Random();
            int minValueForRandom = 0;
            int maxValueForRandom = 1;
            int number = random.Next(minValueForRandom, maxValueForRandom);
            int degreeNumber = 0;
            int numberPower = 1;
            int two = 2;
            bool isPowerTwoLessNumber = true;

            Console.WriteLine($"Рандомное число: {number}");

            while (isPowerTwoLessNumber)
            {
                numberPower *= two;
                degreeNumber++;
                isPowerTwoLessNumber = numberPower <= number;
            }

            Console.WriteLine($"Минимальная степень двойки, превосходящая число {number}: {degreeNumber}");
            Console.ReadKey();
        }
    }
}
