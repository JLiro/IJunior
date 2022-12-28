using System;

namespace Task04SumNumbers
{
    internal class Program
    {
        static void Main()
        {
            Random random = new Random();

            int minValue = 0;
            int maxValue = 100;
            int number = random.Next(minValue, maxValue);
            int multiplicityNumber1 = 3;
            int multiplicityNumber2 = 5;
            int sum = number;

            Console.WriteLine(number);

            for (int i = 0; i < number; i++)
            {
                if (i % multiplicityNumber1 == 0 || i % multiplicityNumber2 == 0)
                {
                    sum += i;
                    Console.Write(i + " ");
                }
            }
            Console.WriteLine(sum);
        }
    }
}
