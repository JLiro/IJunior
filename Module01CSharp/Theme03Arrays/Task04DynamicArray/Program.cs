using System;

namespace Task04DynamicArray
{
    internal class Program
    {
        static void Main()
        {
            const string CommandSum = "sum";
            const string CommandExit = "exit";

            int[] array = new int[0];

            string userText;

            bool isWorking = true;

            while (isWorking)
            {
                Console.Write($"КОМАНДЫ" +
                                  $"\n{CommandSum} : Cложить числа" +
                                  $"\n{CommandExit} : Выход):" +
                                  $"\n" +
                                  $"\nВведите число или команду: ");
                userText = Console.ReadLine().ToLower();

                switch (userText)
                {
                    case CommandSum:
                        int sum = 0;

                        for (int i = 0; i < array.Length; i++)
                        {
                            sum += array[i];
                        }

                        Console.WriteLine($"Сумма чисел: {sum}");
                        Console.ReadKey();
                        break;

                    case CommandExit:
                        isWorking = false;
                        break;

                    default:
                        if (int.TryParse(userText, out int value))
                        {
                            int[] tempArray = new int[array.Length + 1];

                            for (int i = 0; i < array.Length; i++)
                            {
                                tempArray[i] = array[i];
                            }

                            tempArray[tempArray.Length - 1] = value;
                            array = tempArray;
                        }
                        break;
                }

                Console.Clear();
            }
        }
    }
}
