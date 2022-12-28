/*/ 
    - == === ЗАДАЧА === == -
        
    ==0 Легенда:
        Вы приходите в магазин и хотите купить за своё золото кристаллы. В вашем
    кошельке есть какое-то количество золота, продавец спрашивает у вас, сколько
    кристаллов вы хотите купить? После сделки у вас остаётся какое-то количество золота
    и появляется какое-то количество кристаллов.

    ==0 Формально:
        При старте программы пользователь вводит начальное количество золота. Потом ему
    предлагается купить какое-то количество кристаллов по цене N(задать в программе самому).
    Пользователь вводит число и его золото конвертируется в кристаллы. Остаток золота и кристаллов
    выводится на экран.

        Проверять на то, что у игрока достаточно денег ненужно.
/*/

using System;

namespace Task06CrystalShop
{
    internal class Program
    {
        static void Main()
        {
            int сrystalStonesPrice = 10;
            int сrystalStonesCount;
            int goldenСoinsCount;

            Console.Write("Продавец: Сколько у Вас золота?\nВы: ");
            goldenСoinsCount = Convert.ToInt32(Console.ReadLine());
            Console.Write($"\nПродавец: Один кристалл стоит {сrystalStonesPrice} золота!" +
                          $"\n          Сколько кристаллов Вы хотели бы купить?\nВы: ");
            сrystalStonesCount = Convert.ToInt32(Console.ReadLine());

            goldenСoinsCount -= сrystalStonesCount * сrystalStonesPrice; 

            Console.WriteLine($"\nБыло куплено {сrystalStonesCount} кристаллов" +
                              $"\nУ вас осталось {goldenСoinsCount} золота ");

            Console.ReadKey();
        }
    }
}
