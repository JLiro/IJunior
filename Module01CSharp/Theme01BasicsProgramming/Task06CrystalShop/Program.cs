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
