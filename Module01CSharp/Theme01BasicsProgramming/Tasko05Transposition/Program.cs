/*/ 
    - == === ЗАДАЧА === == -
        
        Даны две переменные. Поменять местами значения двух переменных.
    Вывести на экран значения переменных до перестановки и после. К примеру,
    есть две переменные имя и фамилия, они сразу инициализированные,
    но данные не верные, перепутанные. Вот эти данные и надо поменять
    местами через код.
/*/

using System;

namespace Tasko05Transposition
{
    internal class Program
    {
        static void Main()
        {
            string name = "GoodMan";
            string lastName = "Soul";

            string temp = null;

            Console.Write(
                          $"— В Вашей анкете указано имя {name}, фамилия {lastName}. Всё верно?\n" +
                          $"- Наоборот.. фамилия {lastName}, имя {name}\n" +
                          $"- Оу.. Прошу прощения, сейчас всё поправим!\n"
                         );

            temp = name;
            name = lastName;
            lastName = temp;

            Console.Write($"— Ну вот, всё готово, - имя {name}, а фамилия {lastName}!");
            Console.ReadKey();
        }
    }
}
