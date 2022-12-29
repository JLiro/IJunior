using System;

namespace Task02ExitControl
{
    internal class Program
    {
        static void Main()
        {
            string userText = null;
            string exitСommand = "Exit";

            while (userText != exitСommand)
            {
                Console.Write("Введите текст: ");
                userText = Console.ReadLine();
            }
        }
    }
}
