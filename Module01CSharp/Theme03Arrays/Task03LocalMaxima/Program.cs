using System;

namespace Task03LocalMaxima
{
    internal class Program
    {
        static void Main()
        {
            int[] array = new int[30];
            int currentLocalMaximum = 0;

            string allLocalMaximum = "\nСписок локальных максимумов: ";

            Random random = new Random();

            for (int row = 0; row < array.GetLength(0); row++)
            {
                array[row] = random.Next(10, 100);

                Console.Write(array[row] + " ");
            }

            for (int row = 0; row < array.GetLength(0); row++)
            {
                int previousArrayValue = row >= 1 ? array[row - 1] : 0;
                int currentArrayValue = row >= 0 ? array[row] : 0;
                int nextArrayValue = row < array.GetLength(0) - 1 ? array[row + 1] : 0;

                if  (currentArrayValue > nextArrayValue)
                {
                     currentLocalMaximum = (currentArrayValue > previousArrayValue) ? currentArrayValue : previousArrayValue;
                }
                else currentLocalMaximum = (nextArrayValue > previousArrayValue) ? nextArrayValue : previousArrayValue;

                allLocalMaximum += currentLocalMaximum != 0 ? currentLocalMaximum + " " : "";
            }

            Console.WriteLine("\n" + allLocalMaximum);
            Console.ReadKey();
        }
    }
}
