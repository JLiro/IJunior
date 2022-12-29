using System;

namespace Task07OutputName
{
    internal class Program
    {
        static void Main()
        {
            string userName = string.Empty;
            
            string symbolFromBorder = string.Empty,
                          borderRow = string.Empty;
            
            int additionToLengthOfHorizontalBorder = 4;

            Console.Write("Введите Ваше имя: ");
            userName = Console.ReadLine();

            Console.Write("Введите символ: ");
            symbolFromBorder = Console.ReadLine();

            for (int i = 0; i < userName.Length + additionToLengthOfHorizontalBorder; i++)
            {
                borderRow += symbolFromBorder;
            }

            Console.WriteLine($"\n{borderRow}" +
                              $"\n{symbolFromBorder} {userName} {symbolFromBorder}" +
                              $"\n{borderRow}");

            Console.ReadKey();
        }
    }
}
