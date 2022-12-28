using System;

namespace Tasko05Transposition
{
    internal class Program
    {
        static void Main()
        {
            string name = "GoodMan";
            string lastName = "Soul";
            string tempName = null;

            Console.Write(
                          $"— В Вашей анкете указано имя {name}, фамилия {lastName}. Всё верно?\n" +
                          $"- Наоборот.. фамилия {lastName}, имя {name}\n" +
                          $"- Оу.. Прошу прощения, сейчас всё поправим!\n"
                         );

            tempName = name;
            name = lastName;
            lastName = tempName;

            Console.Write($"— Ну вот, всё готово, - имя {name}, а фамилия {lastName}!");
            Console.ReadKey();
        }
    }
}
