using System;

namespace Task05KansasCityShuffle
{
    internal class Program
    {
        static void Main()
        {
            int[] array = { 1, 2, 3, 4, 5 };

            ShowArray(array, "Изначальный массив: ");
            ShuffleArray(array);
            ShowArray(array, "\nПеремешанный массив: ");

            Console.ReadKey();
        }

        private static void ShuffleArray(int[] array)
        {
            Random random = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                int tempValue = array[i];
                int randomIndex = random.Next(i, array.Length);

                array[i] = array[randomIndex];
                array[randomIndex] = tempValue;
            }
        }

        private static void ShowArray(int[] array, string text)
        {
            Console.Write(text);
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i]);
            }
        }
    }
}