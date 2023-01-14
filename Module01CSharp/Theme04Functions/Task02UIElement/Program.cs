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
            int maxHealthSymbol = 10;
            float fulnessHealthBar;
            int horizontalPositionHealthBar;
            int verticalPositionHealthBar;

            string manaBarName = "Мана";
            int maxManaSymbol = 10;
            float fulnessManaBar;
            int horizontalPositionManaBar;
            int verticalPositionManaBar;

            SetSettingsDrawBar(healthBarName, out fulnessHealthBar, out horizontalPositionHealthBar, out verticalPositionHealthBar);
            SetSettingsDrawBar(manaBarName, out fulnessManaBar, out horizontalPositionManaBar, out verticalPositionManaBar);

            DrawBar(healthBarName, maxHealthSymbol, fulnessHealthBar, horizontalPositionHealthBar, verticalPositionHealthBar);
            DrawBar(manaBarName, maxManaSymbol, fulnessManaBar, horizontalPositionManaBar, verticalPositionManaBar);
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

        static void DrawBar(string barName, int maxSymbol, float fullnessBar, int horizontalPosition, int verticalPosition)
        {
            string barView = String.Empty;

            Console.SetCursorPosition(horizontalPosition, verticalPosition);

            for (int i = 0; i < maxSymbol; i++)
            {
                barView += i < Convert.ToInt32(fullnessBar / maxSymbol) ? "#" : "_";
            }

            Console.WriteLine($"[{barView}] {barName}");
        }
    }
}
