/*/ 
    - == === ЗАДАЧА === == -
        
        На экране в специальной зоне выводятся картинки, по 3 в ряд. Всего у пользователя
    в альбоме 52 картинки. Код должен вывести, сколько полностью заполненных рядов можно
    будет вывести, и сколько картинок будет сверх меры.

        В качестве решения ожидается объявленные переменные с необходимыми значениями и 
    вывод необходимых данных основываясь на значениях переменных
/*/

using System;

namespace Task04Picture
{
    internal class Program
    {
        static void Main()
        {
            int countImages = 52;
            int rowSize = 3;
            int filledRows = countImages / rowSize;
            int excessCountImages = countImages % rowSize;

            Console.WriteLine($"Полных рядов: {filledRows}\n" +
                              $"Картинок сверх меры: {excessCountImages}");
            Console.ReadKey();
        }
    }
}
