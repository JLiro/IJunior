using System;

namespace Task04Picture
{
    internal class Program
    {
        static void Main()
        {
            int imagesCount = 52;
            int rowSize = 3;
            int rowsFilling = imagesCount / rowSize;
            int excessImagesCount = imagesCount % rowSize;

            Console.WriteLine($"Полных рядов: {rowsFilling}\n" +
                              $"Картинок сверх меры: {excessImagesCount}");
            Console.ReadKey();
        }
    }
}
