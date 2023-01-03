using System;

namespace Task08ShiftArrayValues
{
    internal class Program
    {
        static void Main()
        {
            int arraySize = 10;
            int[] array = new int[arraySize];

            int firstElementArray;
            int shiftPositionValue;

            Random random = new Random();
            int randomMin = 10;
            int randomMax = 100;


            Console.WriteLine("Исходный массив:");

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(randomMin, randomMax);
                Console.Write(array[i] + " ");
            }

            Console.Write("\n\nВведите число для сдвига ячеек массива влево: ");
            shiftPositionValue = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < shiftPositionValue; i++)
            {
                firstElementArray = array[0];

                for (int j = 1; j < array.Length; j++)
                {
                    array[j - 1] = array[j];
                }
                
                array[array.Length - 1] = firstElementArray;
            }

            Console.WriteLine($"\nРезультат сдвига на {shiftPositionValue} ячейки:\n");

            foreach (int number in array)
            {
                Console.Write(number + " ");
            }

            Console.ReadKey();
        }
    }
}
