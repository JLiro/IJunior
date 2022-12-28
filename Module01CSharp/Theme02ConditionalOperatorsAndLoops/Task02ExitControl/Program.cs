using System;

namespace Task02ExitControl
{
    internal class Program
    {
        static void Main()
        {
            string userText = null;

            while (userText != "Exit")
            {
                Console.Write("Введите текст: ");
                userText = Console.ReadLine();
            }
        }
    }
}
