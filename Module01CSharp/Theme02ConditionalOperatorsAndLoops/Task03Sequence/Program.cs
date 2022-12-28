﻿/*/ 
    - == === ЗАДАЧА === == -
        
        Нужно написать программу (используя циклы, обязательно
    пояснить выбор вашего цикла), чтобы она выводила следующую
    последовательность 5 12 19 26 33 40 47 54 61 68 75 82 89 96
    Нужны переменные для обозначения чисел в условии цикла.
/*/

using System;

namespace Task03Sequence
{
    internal class Program
    {
        static void Main()
        {
            for (int i = 5; i < 97; i += 7)
            {
                Console.Write(i + " ");
            }

            Console.ReadKey();
            
            /*/
             * Выбран цикл "for", т.к. в нём явно указываются:
             *      1. Начало отсчёта;
             *      2. Условие выполнения;
             *      3. Шаг цикла;
            /*/
        }
    }
}
