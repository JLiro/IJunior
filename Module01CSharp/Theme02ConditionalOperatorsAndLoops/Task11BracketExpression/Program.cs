using System;

namespace Task11BracketExpression
{
    internal class Program
    {
        static void Main()
        {
            int leftBracketsCount = 0;
            int rightBracketsCount= 0;

            string bracketExpression;

            bool isCorrectBracketExpression;
            bool isOpenBracket;
            bool isClosedBracket;
            bool isOpeningPartExpression;

            Console.Write("Введите последовательность скобок: ");
            bracketExpression = Console.ReadLine();

            foreach (char bracket in bracketExpression)
            {
                isOpenBracket = bracket == '(';
                isClosedBracket = bracket == ')';

                if (isOpenBracket && isOpeningPartExpression) { leftBracketsCount++; }
                else if (isClosedBracket) { rightBracketsCount++; }
            }

            isCorrectBracketExpression = leftBracketsCount == rightBracketsCount;
            if (isCorrectBracketExpression)
            {
                Console.WriteLine($"\nЭто корректное скобочное выражение с глубиной {leftBracketsCount}");
            }
                else Console.WriteLine("Это некорректное скобочное выражение");

            Console.ReadKey();
        }
    }
}
