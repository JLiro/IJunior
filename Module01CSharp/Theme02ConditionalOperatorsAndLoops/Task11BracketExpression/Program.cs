using System;

namespace Task11BracketExpression
{
    internal class Program
    {
        static void Main()
        {
            int leftBracketsCount = 0,
                rightBracketsCount= 0;

            string bracketExpression;

            Console.Write("Введите последовательность скобок: ");
            bracketExpression = Console.ReadLine();

            foreach (char bracket in bracketExpression)
            {
                if (bracket == '(') { leftBracketsCount++; }
                else if (bracket == ')') { rightBracketsCount++; }
            }

            if (leftBracketsCount == rightBracketsCount) Console.WriteLine($"\nЭто корректное скобочное выражение с глубиной {leftBracketsCount}");
            else Console.WriteLine("Это некорректное скобочное выражение");

            Console.ReadKey();
        }
    }
}
