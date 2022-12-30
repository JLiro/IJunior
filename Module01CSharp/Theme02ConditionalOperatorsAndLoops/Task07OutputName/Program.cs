using System;

namespace Task07OutputName
{
    internal class Program
    {
        static void Main()
        {
            string userName = string.Empty;

            char symbolFromBorder;
            string borderRow = string.Empty;
            string nameRow = string.Empty;

            Console.Write("Введите Ваше имя: ");
            userName = Console.ReadLine();

            Console.Write("Введите символ: ");
            symbolFromBorder = Convert.ToChar(Console.ReadLine());

            nameRow = symbolFromBorder + userName + symbolFromBorder;
            for (int i = 0; i < nameRow.Length; i++)
            {
                borderRow += symbolFromBorder;
            }

            Console.WriteLine($"\n{borderRow}" +
                              $"\n{nameRow}" +
                              $"\n{borderRow}");

            Console.ReadKey();
        }
    }
}
