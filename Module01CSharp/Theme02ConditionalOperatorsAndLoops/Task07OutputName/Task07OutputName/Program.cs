/*/ 
    - == === ЗАДАЧА === == -
        
        Вывести имя в прямоугольник из символа, который введет сам пользователь.

        Вы запрашиваете имя, после запрашиваете символ, а после отрисовываете
    в консоль его имя в прямоугольнике из его символов.

        Пример:

        Alexey

        %

        %%%%%%%%%%
        % Alexey %
        %%%%%%%%%%
/*/

using System;

namespace Task07OutputName
{
    internal class Program
    {
        static void Main()
        {
            string userName = null;
            string symbolFrame = null;
            int wordBoxIndent;

            Console.Write("Введите Ваше имя: ");
            userName = Console.ReadLine();

            Console.Write("Введите символ: ");
            symbolFrame = Console.ReadLine();

            for (int i = 0; i < userName.Length + 4; i++)
            {
                symbolFrame += symbolFrame;
            }

            Console.WriteLine($"\n{symbolFrame}\n" +
                              $"% {userName} % \n" +
                              $"{symbolFrame}");

            Console.ReadKey();
        }
    }
}
