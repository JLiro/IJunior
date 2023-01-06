using System;

namespace Task05KansasCityShuffle
{
    internal class Program
    {
        static void Main()
        {
            int[] array = { 1, 2, 3, 4, 5 };

            ViewArray(array, "Изначальный массив: ");
            ShuffleArray(ref array);
            ViewArray(array, "\nПеремешанный массив: ");

            Console.ReadKey();
        }

        public static void ShuffleArray(ref int[] array)
        {
            Random random = new Random();

            for (int index = 0; index < array.Length; index++)
            {
                int tempArray = array[index];
                int randomIndex = random.Next(index, array.Length);

                array[index] = array[randomIndex];
                array[randomIndex] = tempArray;
            }
        }

        public static void ViewArray(int[] array, string text)
        {
            Console.Write(text);
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i]);
            }
        }
    }
}