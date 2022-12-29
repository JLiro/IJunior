using System;

namespace Task03Sequence
{
    internal class Program
    {
        static void Main()
        {
            int startingNumber = 5,
                  finishNumber = 97,
                      loopStep = 7;

            for (int i = startingNumber; i < finishNumber; i += loopStep)
            {
                Console.Write(i + " ");
            }

            Console.Write("\n\nВыбран цикл for, т.к. в нём явно указываются:"
                          + "\n1. Начало отсчёта;"
                          + "\n2. Условие выполнения;"
                          + "\n3. Шаг цикла.");
            Console.ReadKey();
        }
    }
}
