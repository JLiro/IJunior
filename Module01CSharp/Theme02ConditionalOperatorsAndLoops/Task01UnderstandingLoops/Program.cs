/*/ 
    - == === ЗАДАЧА === == -
        
        При помощи циклов вы можете повторять один и тот же код множество раз.
    Напишите простейшую программу, которая выводит указанное(установленное)
    пользователем сообщение заданное количество раз. Количество повторов
    также должен ввести пользователь.
/*/

using System;

namespace Task01UnderstandingLoops
{
    internal class Program
    {
        static void Main()
        {
            string userText;
            int numberIterationsLoop;

            Console.Write("Введите текст: ");
            userText = Console.ReadLine();
            Console.Write("Введите сколько раз вывести текст: ");
            numberIterationsLoop = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            for (int i = 0; i < numberIterationsLoop; i++)
            {
                Console.WriteLine(userText);
            }

            Console.ReadKey();
        }
    }
}
