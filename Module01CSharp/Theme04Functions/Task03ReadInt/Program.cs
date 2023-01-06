using System;

namespace Task03ReadInt
{
    internal class Program
    {
        static void Main()
        {
            int userNumber;

            userNumber = GetNumber();
            Console.Clear();

            Console.Write($"Число {userNumber} успешно сконвертировано!");
            Console.ReadKey();
        }

        static int GetNumber()
        {
            bool isNumber = false;
            int userNumber = 0;

            while (isNumber == false)
            {
                Console.Write("Ввидите число для конвертации: ");
                isNumber = int.TryParse(Console.ReadLine(), out userNumber);
            }

            return userNumber;
        }
    }
}