using System;

namespace Task04SumNumbers
{
    internal class Program
    {
        static void Main()
        {
            Random random = new Random();

            int maxValue = 100;
            int number = random.Next(0, maxValue);
            int divider0 = 3;
            int divider1 = 5;
            int sum = number;

            Console.WriteLine(number);

            for (int i = 0; i < number; i++)
            {
                if (i % divider0 == 0 || i % divider1 == 0)
                {
                    sum += i;
                    Console.Write(i + " ");
                }
            }
            Console.WriteLine(sum);
        }
    }
}
