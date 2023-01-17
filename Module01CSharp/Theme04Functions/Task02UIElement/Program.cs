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
            int maxHealthSymbolCount = 10;
            float fulnessHealthBar;
            int horizontalPositionHealthBar;
            int verticalPositionHealthBar;

            string manaBarName = "Мана";
            int maxManaSymbolCount = 10;
            float fulnessManaBar;
            int horizontalPositionManaBar;
            int verticalPositionManaBar;

            SetSettingsDrawBar(healthBarName, out fulnessHealthBar, out horizontalPositionHealthBar, out verticalPositionHealthBar);
            SetSettingsDrawBar(manaBarName, out fulnessManaBar, out horizontalPositionManaBar, out verticalPositionManaBar);

            DrawBar(healthBarName, maxHealthSymbolCount, fulnessHealthBar, horizontalPositionHealthBar, verticalPositionHealthBar);
            DrawBar(manaBarName, maxManaSymbolCount, fulnessManaBar, horizontalPositionManaBar, verticalPositionManaBar);
        }

        static void SetSettingsDrawBar(string barName, out float fullnessBar, out int horizontalPosition, out int verticalPosition)
        {
            Console.Write($"Введите заполненность бара {barName} в процентах от 0 до 100: ");
            fullnessBar = Convert.ToInt32(Console.ReadLine());

            Console.Write($"Введите позицию бара [{barName}] по X: ");
            horizontalPosition = Convert.ToInt32(Console.ReadLine());
            Console.Write($"Введите позицию бара [{barName}] по Y: ");
            verticalPosition = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
        }

        static void DrawBar(string barName, int maxSymbolCount, float fullnessBar, int horizontalPosition, int verticalPosition)
        {
            string barView = String.Empty;
            int symbolCount = Convert.ToInt32(fullnessBar / maxSymbolCount);

            if (symbolCount > maxSymbolCount)
            {
                symbolCount = 10;
            }
            else if (symbolCount < 0)
            {
                symbolCount = 0;
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
