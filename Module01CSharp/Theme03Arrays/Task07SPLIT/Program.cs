using System;

namespace Task07SPLIT
{
    internal class Program
    {
        static void Main()
        {
            string text = "Дана строка с текстом, используя метод строки String.Split() получить массив слов";
            string[] words = text.Split(' ');

            foreach (var word in words)
            {
                Console.WriteLine(word);
            }

            Console.ReadLine();
        }
    }
}
