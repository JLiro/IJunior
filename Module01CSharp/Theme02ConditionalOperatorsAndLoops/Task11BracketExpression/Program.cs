using System;

namespace Task11BracketExpression
{
    internal class Program
    {
        static void Main()
        {
            char OpenBracket = '(';

            string bracketsString;

            int bracketsBalance = 0;
            int bracketsMaxDepth = 0;

            Console.Write("Введите строку символов: ");
            bracketsString = Console.ReadLine();

            foreach (char symbol in bracketsString)
            {
                if (bracketsBalance < 0)
                {
                    break;
                }

                if (symbol == OpenBracket)
                {
                    bracketsBalance++;
                }
                else
                {
                    bracketsBalance--;
                }    

                if (bracketsBalance > bracketsMaxDepth)
                {
                    bracketsMaxDepth = bracketsBalance;
                }
            }

            if (bracketsBalance == 0)
            {
                Console.WriteLine($"Строка корректна. Максимальная глубина вложенности скобок: {bracketsMaxDepth}");
            }
            else
            {
                Console.WriteLine("Строка не корректна");
            }

            Console.ReadKey();
        }
    }
}
