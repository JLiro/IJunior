using System;

namespace Task01WorkingWithSpecificRowsAndColumns
{
    internal class Program
    {
        static void Main()
        {
            int[,] array = { { 1, 2, 3 },
                             { 3, 4, 5 },
                             { 5, 6, 6 } };

            int sumNumbersFromRow;
            int productNumbersFromColumn;

            int columnNumberForCalculatingAmount = 1;
            int columnNumber = 0;

            productNumbersFromColumn = 1;
            sumNumbersFromRow = 0;
            for (int column = 0; column < array.GetLength(0); column++)
            {
                productNumbersFromColumn *= array[column, columnNumber];

                for (int row = 0; row < array.GetLength(1); row++)
                {
                    sumNumbersFromRow += column == columnNumberForCalculatingAmount ? array[column, row] : 0;

                    Console.Write(array[column, row] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine($"\nСумма второй строки: {sumNumbersFromRow}" +
                              $"\nПроизведение первого столбца: {productNumbersFromColumn}");
            Console.ReadKey();
        }
    }
}
