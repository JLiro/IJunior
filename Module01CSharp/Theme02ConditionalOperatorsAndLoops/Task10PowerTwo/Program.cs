using System;

namespace Task10PowerTwo
{
    internal class Program
    {
        static void Main()
        {
            Random random = new Random();
            int minValueForRandom = 1;
            int maxValueForRandom = 28;
            int number = random.Next(minValueForRandom, maxValueForRandom);
            int degreeNumber = 1;
            int two = 2;
            bool isPowerTwoLessNumber;

            Console.WriteLine($"Рандомное число: {number}");

            isPowerTwoLessNumber = degreeNumber <= number;

            while (isPowerTwoLessNumber)
            {
                degreeNumber *= two;
            }

            Console.WriteLine($"Минимальная степень двойки, превосходящая число {number}: {degreeNumber}");
            Console.ReadKey();
        }
    }
}
