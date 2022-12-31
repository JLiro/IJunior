using System;

namespace Task11BracketExpression
{
    internal class Program
    {
        static void Main()
        {
            char openBracket = '(';
            char closeBracket = ')';

            int openBracketsCount = 0;
            int closedBracketsCount= 0;

            string bracketsExpression;

            bool isOpenBracket;
            bool isClosedBracket;
            bool isCorrectBracketExpression;
            bool isFirstSymbolCloseBracket;
            bool isLastSymbolOpenBracket;
            bool isEqualOpenAndCloseBracketsNumber;

            Console.Write("Введите последовательность скобок: ");
            bracketsExpression = Console.ReadLine();

            foreach (char bracket in bracketsExpression)
            {
                isOpenBracket = bracket == openBracket;
                isClosedBracket = bracket == closeBracket;

                if (isOpenBracket) openBracketsCount++;
                if (isClosedBracket) closedBracketsCount++;
            }

            isLastSymbolOpenBracket = bracketsExpression[0] != closeBracket;
            isFirstSymbolCloseBracket = bracketsExpression[bracketsExpression.Length - 1] != openBracket;
            isEqualOpenAndCloseBracketsNumber = openBracketsCount == closedBracketsCount;

            isCorrectBracketExpression = (isFirstSymbolCloseBracket && isLastSymbolOpenBracket && isEqualOpenAndCloseBracketsNumber);

            if (isCorrectBracketExpression)
            {
                Console.Write($"\nЭто корректное скобочное выражение с глубиной: {openBracketsCount}");
            }
                else Console.Write("\nЭто некорректное скобочное выражение");
            Console.ReadKey();
        }
    }
}
