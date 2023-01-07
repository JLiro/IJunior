using System;
using System.Collections.Generic;

namespace Task03DynamicArrayAdvanced
{
    internal class Program
    {
        static void Main()
        {
            const string CommandSum  = "sum";
            const string CommandExit = "ext";

            List<int> numbers = new List<int>();

            string input;

            bool isWork = true;

            while (isWork)
            {
                Console.Write($"КОМАНДЫ" +
                              $"\n{CommandSum} : Cложить числа" +
                              $"\n{CommandExit} : Выход:" +
                              $"\n" +
                              $"\nВведите число или команду: ");
                input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case CommandSum:
                        Console.WriteLine($"Сумма чисел: {Sum(numbers)}");
                        Console.ReadKey();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        AddNumber(numbers, input);
                        break;
                }

                Console.Clear();
            }
        }

        static void AddNumber(List<int> numbers, string input)
        {
            if (int.TryParse(input, out int value))
            {
                numbers.Add(value);
            }
        }

        static int Sum(List<int> numbers)
        {
            int sum = 0;

            foreach (int number in numbers)
            {
                sum += number;
            }
            return sum;
        }
    }
}
