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
            int firstDivider = 3;
            int secondDivider = 5;
            int sum = 0;

            Console.WriteLine(number);
            
            sum += number;
            for (int i = 0; i < number; i++)
            {
                if (i % firstDivider == 0 || i % secondDivider == 0)
                {
                    sum += i;
                    Console.Write(i + " ");
                }
            }
            Console.WriteLine("\n"+sum);
            Console.ReadKey();
        }
    }
}
