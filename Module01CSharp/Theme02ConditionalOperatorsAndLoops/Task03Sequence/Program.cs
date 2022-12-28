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

            Console.Write("\n\nВыбран цикл for, т.к. в нём явно указываются:"
                          + "\n1. Начало отсчёта;"
                          + "\n2. Условие выполнения;"
                          + "\n3. Шаг цикла;");
            Console.ReadKey();
        }
    }
}
