using System;

namespace Task01WorkingWithSpecificRowsAndColumns
{
    internal class Program
    {
        static void Main()
        {
            int[,] array = { { 1, 2, 3 },
                             { 3, 1, 5 },
                             { 5, 6, 6 } };

            int sumNumbersFromRow;
            int productNumbersFromColumn;

            int columnNumberForCalculatingAmount = 1;
            int columnNumber = 0;


            productNumbersFromColumn = 1;
            sumNumbersFromRow = 0;

            for (int column = 0; column < array.GetLength(0); column++)
            {
                for (int row = 0; row < array.GetLength(1); row++)
                {
                    Console.Write(array[column, row] + " ");
                }
                Console.WriteLine();
            }

            for (int row = 0; row < array.GetLength(1); row++)
            {
                sumNumbersFromRow += array[columnNumberForCalculatingAmount, row];
            }

            for (int column = 0; column < array.GetLength(0); column++)
            {
                productNumbersFromColumn *= array[column, columnNumber];
            }

            Console.WriteLine($"\nСумма второй строки: {sumNumbersFromRow}" +
                              $"\nПроизведение первого столбца: {productNumbersFromColumn}");
            Console.ReadKey();
        }
    }
}
