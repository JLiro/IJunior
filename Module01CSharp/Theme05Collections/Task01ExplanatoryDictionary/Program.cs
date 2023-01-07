using System;
using System.Collections.Generic;

namespace Task01ExplanatoryDictionary
{
    internal class Program
    {
        static void Main()
        {
            string input;

            Dictionary<string, string> word = new Dictionary<string, string>();
            word.Add("словарь", "- тип коллекции. Словарь хранит объекты, которые\n" +
                     "представляют пару ключ-значение. Класс словаря Dictionary<K, V>\n" +
                     "типизируется двумя типами: параметр K представляет тип ключей,\n" +
                     "а параметр V предоставляет тип значений.\n");

            Console.Write("Введите слово: ");
            input = Console.ReadLine().ToLower();

            if (word.ContainsKey(input))
            {
                Console.Clear();
                Console.Write($"Словарь {word[input]}");
            }
            else Console.WriteLine("Ошибка! Неизвестное слово!");

            Console.ReadKey();
        }
    }
}
