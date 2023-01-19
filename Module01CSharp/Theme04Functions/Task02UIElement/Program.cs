using System;

namespace Task02UIElement
{
    internal class Program
    {
        static void Main()
        {
            SpawnBars();
            Console.ReadKey();
        }

        static void SpawnBars()
        {
            string healthBarName = "Жизни";
            float fulnessHealthBar;
            int horizontalPositionHealthBar;
            int verticalPositionHealthBar;

            string manaBarName = "Мана";
            float fulnessManaBar;
            int horizontalPositionManaBar;
            int verticalPositionManaBar;

            int maxSymbolCount = 10;
            int minSymbolCount = 0;

            SetSettingsDrawBar(healthBarName, out fulnessHealthBar, out horizontalPositionHealthBar, out verticalPositionHealthBar);
            SetSettingsDrawBar(manaBarName, out fulnessManaBar, out horizontalPositionManaBar, out verticalPositionManaBar);

            DrawBar(healthBarName, maxSymbolCount, minSymbolCount, fulnessHealthBar, horizontalPositionHealthBar, verticalPositionHealthBar);
            DrawBar(manaBarName, maxSymbolCount, minSymbolCount, fulnessManaBar, horizontalPositionManaBar, verticalPositionManaBar);
        }

        static void SetSettingsDrawBar(string barName, out float fullnessBar, out int horizontalPosition, out int verticalPosition)
        {
            int minPercentage = 0;
            int maxPercentage = 100;

            Console.Write($"Введите заполненность бара {barName} в процентах от {minPercentage} до {maxPercentage}: ");
            fullnessBar = Convert.ToInt32(Console.ReadLine());

            Console.Write($"Введите позицию бара [{barName}] по X: ");
            horizontalPosition = Convert.ToInt32(Console.ReadLine());
            Console.Write($"Введите позицию бара [{barName}] по Y: ");
            verticalPosition = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
        }

        static void DrawBar(string barName, int maxSymbolCount, int minSymbolCount, float fullnessBar, int horizontalPosition, int verticalPosition)
        {
            string barView = String.Empty;
            int symbolCount = Convert.ToInt32(fullnessBar / maxSymbolCount);

            if (symbolCount > maxSymbolCount)
            {
                symbolCount = maxSymbolCount;
            }
            else if (symbolCount < minSymbolCount)
            {
                symbolCount = minSymbolCount;
            }

            Console.SetCursorPosition(horizontalPosition, verticalPosition);

            for (int i = 0; i < symbolCount; i++)
            {
                barView += "#";
            }

            int emptinessBar = maxSymbolCount - symbolCount;

            for (int i = 0; i < emptinessBar; i++)
            {
                barView += "_";
            }

            Console.WriteLine($"[{barView}] {barName}");
        }
    }
}
