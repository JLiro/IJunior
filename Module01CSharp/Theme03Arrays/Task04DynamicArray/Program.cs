using System;

namespace Task04DynamicArray
{
    internal class Program
    {
        static void Main()
        {
            int[] array = new int[0];
            string userText = null;

            while (userText != "Exit")
            {
                Console.Write("Введите число или команду: ");
                userText = Console.ReadLine();

                try
                {
                    addNumber(ref array, Convert.ToInt32(userText));
                }
                catch
                {
                    if (userText == "sum")
                    {
                        Console.WriteLine();
                        showArray(array);
                        Console.WriteLine();
                        sum(array);

                        Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить");
                        Console.ReadKey();
                    }
                }
                Console.Clear();
            }
        }

        static void addNumber(ref int[] array, int number)
        {
            int[] arrayTemp = new int[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
                arrayTemp[i] = array[i];

            arrayTemp[array.Length] = number;

            array = arrayTemp;
        }

        static void sum(int[] array)
        {
            int sum = 0;

            for (int i = 0; i < array.Length; i++)
                sum += array[i];

            Console.WriteLine($"Сумма чисел: {sum}");
        }

        static void showArray(int[] array)
        {
            foreach (int item in array) Console.Write(item + " ");
        }
    }
}
