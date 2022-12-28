/*/ 
    - == === ЗАДАЧА === == -
        
        Написать программу, которая будет выполняться до тех пор, пока не будет введено слово exit.
    Помните, в цикле должно быть условие, которое отвечает за то, когда цикл должен завершиться.
    Это нужно, чтобы любой разработчик взглянув на ваш код, понял четкие границы вашего цикла.
/*/

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
